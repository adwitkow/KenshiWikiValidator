using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.ArticleValidation.Locations
{
    internal class LocationsArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public LocationsArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new StringIdRule(itemRepository, wikiTitles),
            };
        }

        public override string CategoryName => "Locations"; // TODO: Should be melee weapons, actually

        protected override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
