using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapon.Rules
{
    internal class ContainsWeaponNavboxRule : ContainsTemplateRuleBase
    {
        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            return new WikiTemplate("Navbox/Weapons", new SortedList<string, string?>());
        }
    }
}
