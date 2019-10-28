using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace AlloyDemoKit.Locations
{
    [Component]
    public class LocationTreeComponent : ComponentDefinitionBase
    {
        public LocationTreeComponent() : base("epi-cms/asset/HierarchicalList")
        {
            base.Title = "Locations";
            Categories = new[] {"content"};
            PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup };
            SortOrder = 1000;
            base.Settings.Add(new Setting("repositoryKey", "locations"));

            base.Settings.Add(new Setting("noDataMessages", new { single = "This folder does not contain any locations", multiple = "These folders do not contain any locations" }));
        }
    }
}