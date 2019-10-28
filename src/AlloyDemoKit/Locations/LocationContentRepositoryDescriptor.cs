using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace AlloyDemoKit.Locations
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class LocationContentRepositoryDescriptor : ContentRepositoryDescriptorBase
    {
        private readonly ContentReference _root;

        public LocationContentRepositoryDescriptor(ContentRootService contentRootService)
        {
            _root = contentRootService.Get("Locations");
        }

        public override string Key => "locations";
        public override string Name => "Locations";
        public override IEnumerable<ContentReference> Roots => new[] { _root };
        public override IEnumerable<Type> ContainedTypes => new[] { typeof(LocationContent), typeof(ContentFolder) };

        public override IEnumerable<Type> CreatableTypes => new[] { typeof(LocationContent), typeof(ContentFolder) };
        public override IEnumerable<Type> LinkableTypes => new[] { typeof(LocationContent), };
        public override IEnumerable<Type> MainNavigationTypes => new[] { typeof(ContentFolder), };

        public override string SearchArea => "CMS/Locations";
    }
}