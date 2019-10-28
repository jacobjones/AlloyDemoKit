using EPiServer.DataAbstraction;
using EPiServer.Find.Cms.SearchProviders;
using EPiServer.Find.Framework.UI.Localization;
using EPiServer.Framework.Localization;
using EPiServer.Shell;
using EPiServer.Shell.Search;
using EPiServer.Web;

namespace AlloyDemoKit.Locations
{
    [SearchProvider]
    public class LocationSearchProvider : EnterpriseContentSearchProviderBase<LocationContent, ContentType>
    {
        public LocationSearchProvider(LocalizationService localizationService,
            ISiteDefinitionResolver siteDefinitionResolver, IContentTypeRepository blockTypeRepository,
            UIDescriptorRegistry uiDescriptorRegistry) : base(localizationService, siteDefinitionResolver,
            blockTypeRepository, uiDescriptorRegistry)
        {
        }

        protected override string IconCssClass(LocationContent contentData)
        {
            return FindContentSearchProviderConstants.BlockIconCssClass;
        }

        public override string Category => "Locations";

        public override string Area => "CMS/Locations";
    }
}