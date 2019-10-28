using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace AlloyDemoKit.Locations
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class LocationRootInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var contentRootService = context.Locate.Advanced.GetInstance<ContentRootService>();

            contentRootService.Register<ContentFolder>("Locations", new Guid("3A596E25-4B13-41D6-9958-C21D38B71A09"),
                ContentReference.RootPage);
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }
}