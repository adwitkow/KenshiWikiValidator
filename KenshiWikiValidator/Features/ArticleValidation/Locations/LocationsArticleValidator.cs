using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.ArticleValidation.Locations
{
    internal class LocationsArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public LocationsArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Town"),
                new StringIdRule(itemRepository, wikiTitles, true, ItemType.Town),
            };
        }

        public override string CategoryName => "Locations";

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
