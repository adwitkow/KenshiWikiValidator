using KenshiWikiValidator.Features.ArticleValidation.Locations.Rules;
using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.ArticleValidation.Locations
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
