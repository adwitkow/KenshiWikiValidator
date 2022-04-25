using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.Locations.Rules;
using KenshiWikiValidator.WikiCategories.SharedRules;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.WikiCategories.Locations
{
    internal class LocationsArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public LocationsArticleValidator(IItemRepository itemRepository, ZoneDataProvider zoneDataProvider, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Town"),
                new StringIdRule(itemRepository, wikiTitles, true, ItemType.Town),
                new ContainsTownTemplateRule(itemRepository, wikiTitles, zoneDataProvider),
            };
        }

        public override string CategoryName => "Locations";

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
