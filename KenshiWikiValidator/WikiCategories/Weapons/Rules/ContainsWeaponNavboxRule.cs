using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.WikiTemplates;

namespace KenshiWikiValidator.WikiCategories.Weapons.Rules
{
    internal class ContainsWeaponNavboxRule : ContainsDetailedTemplateRuleBase
    {
        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            return new WikiTemplate("Navbox/Weapons", new SortedList<string, string?>());
        }
    }
}
