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
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }
}