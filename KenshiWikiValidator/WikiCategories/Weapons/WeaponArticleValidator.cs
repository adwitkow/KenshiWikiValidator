using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.SharedRules;
using KenshiWikiValidator.WikiCategories.Weapons.Rules;

namespace KenshiWikiValidator.WikiCategories.Weapons
{
    public class WeaponArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new StringIdRule(itemRepository, wikiTitles),
                new ContainsTemplateRule("Weapon", new[] { "Weapon Types" }),
                new NewLinesRule(),
                new ContainsBlueprintsSectionRule(itemRepository, wikiTitles),
                new ContainsWeaponTemplateRule(itemRepository),
                new ContainsWeaponNavboxRule(),
                new ContainsWeaponCraftingSectionRule(itemRepository),
            };
        }

        public override string CategoryName => "Weapons"; // TODO: Should be melee weapons, actually

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}