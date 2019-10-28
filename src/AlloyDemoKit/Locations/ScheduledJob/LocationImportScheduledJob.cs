using System;
using System.IO;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Globalization;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using Newtonsoft.Json;

namespace AlloyDemoKit.Locations.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "Import Locations")]
    public class LocationImportScheduledJob : ScheduledJobBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ContentRootService _contentRootService;
        private readonly IContentRepository _contentRepository;
        private readonly IUrlSegmentGenerator _urlSegmentGenerator;

        private bool _isStopped;
        private int _folderCount;
        private int _locationCount;

        public LocationImportScheduledJob(IHostingEnvironment hostingEnvironment, ContentRootService contentRootService, IContentRepository contentRepository, IUrlSegmentGenerator urlSegmentGenerator)
        {
            _hostingEnvironment = hostingEnvironment;
            _contentRootService = contentRootService;
            _contentRepository = contentRepository;
            _urlSegmentGenerator = urlSegmentGenerator;

            IsStoppable = true;
        }

        public override void Stop()
        {
            _isStopped = true;
        }

        private string StatusMessage => $"{_folderCount} folders and {_locationCount} locations imported.";

        private ContentFolder GetFolderBySegment(ContentReference rootLink, string regionName)
        {
            return _contentRepository.GetBySegment(rootLink, _urlSegmentGenerator.Create(regionName), ContentLanguage.Instance.FinalFallbackCulture) as ContentFolder;
        }

        public override string Execute()
        {
            // Open and deserialize our locations JSON file
            var path = _hostingEnvironment.MapPath("~/episerver-locations.json");

            LocationDto[] locationDtos;

            using (var sr = new StreamReader(path))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    locationDtos = new JsonSerializer().Deserialize<LocationDto[]>(reader);
                }
            }

            if (_isStopped)
            {
                return "Stopped before any venues were imported.";
            }

            // Get the content root
            var rootLink = _contentRootService.Get("Locations");

            foreach (var regionLocation in locationDtos.GroupBy(x => x.Region))
            {
                if (_isStopped)
                {
                    return $"Stopped: {StatusMessage}";
                }

                var folder = GetFolderBySegment(rootLink, regionLocation.Key);

                if (folder == null)
                {
                    folder =_contentRepository.GetDefault<ContentFolder>(rootLink);
                    folder.Name = regionLocation.Key;
                    _contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);
                    _folderCount++;
                }

                var locationNames = _contentRepository.GetChildren<LocationContent>(folder.ContentLink)
                    .Select(x => x.LocationName).ToList();

                foreach (var locationDto in regionLocation)
                {
                    if (_isStopped)
                    {
                        return $"Stopped: {StatusMessage}";
                    }

                    if (locationNames.Contains(locationDto.LocationName))
                    {
                        continue;
                    }

                    var location = _contentRepository.GetDefault<LocationContent>(folder.ContentLink);
                    location.Name = locationDto.LocationName;
                    location.LocationName = locationDto.LocationName;
                    location.Description = locationDto.Description;
                    location.AddressLine1 = locationDto.AddressLine1;
                    location.AddressLine2 = locationDto.AddressLine2;
                    location.City = locationDto.City;
                    location.Subdivision = locationDto.Subdivision;
                    location.PostalCode = locationDto.PostalCode;
                    location.Country = locationDto.Country;

                    _contentRepository.Save(location, SaveAction.Publish, AccessLevel.NoAccess);
                    _locationCount++;
                }
            }


            return $"Success: {StatusMessage}";
        }
    }
}
