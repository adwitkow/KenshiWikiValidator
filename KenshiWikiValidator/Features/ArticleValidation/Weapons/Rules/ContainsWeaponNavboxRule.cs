using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapons.Rules
{
    internal class ContainsWeaponNavboxRule : ContainsDetailedTemplateRuleBase
    {
        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            return new WikiTemplate("Navbox/Weapons", new SortedList<string, string?>());
        }
    }
}
