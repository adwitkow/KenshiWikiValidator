using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.Locations.Templates;
using KenshiWikiValidator.WikiTemplates;

namespace KenshiWikiValidator.WikiCategories.Locations.Rules
{
    public class ContainsTownTemplateRule : ContainsDetailedTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitles;
        private readonly ZoneDataProvider zoneDataProvider;

        public ContainsTownTemplateRule(IItemRepository itemRepository, WikiTitleCache wikiTitles, ZoneDataProvider zoneDataProvider)
        {
            this.itemRepository = itemRepository;
            this.wikiTitles = wikiTitles;
            this.zoneDataProvider = zoneDataProvider;
        }

        protected override WikiTemplate? PrepareTemplate(ArticleData data)
        {
            var weaponTemplateCreator = new TownTemplateCreator(this.itemRepository, this.zoneDataProvider, this.wikiTitles, data);

            return weaponTemplateCreator.Generate();
        }
    }
}
