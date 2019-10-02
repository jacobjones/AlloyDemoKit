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

        // TODO: Replace the following:
        public override string Key { get; }
        public override string Name { get; }
        public override IEnumerable<ContentReference> Roots { get; }
        public override IEnumerable<Type> ContainedTypes { get; }
    }
}