using KenshiWikiValidator.Features.ArticleValidation;
using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.OcsProxy;

namespace KenshiWikiValidator.Features.CharacterValidation
{
    internal class CharactersArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public CharactersArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Character"),
            };
        }

        public override string CategoryName => "Characters";

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
