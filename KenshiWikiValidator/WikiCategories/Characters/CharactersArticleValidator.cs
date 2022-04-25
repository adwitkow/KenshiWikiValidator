using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.SharedRules;

namespace KenshiWikiValidator.WikiCategories.Characters
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
