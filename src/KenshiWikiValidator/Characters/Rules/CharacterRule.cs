using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.Characters.Templates;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Characters.Rules
{
    public class CharacterRule : ContainsDetailedTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly CharacterTemplateCreator templateCreator;

        public CharacterRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.templateCreator = new CharacterTemplateCreator(itemRepository);
        }

        protected override WikiTemplate? PrepareTemplate(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().SingleOrDefault();

            if (string.IsNullOrEmpty(stringId))
            {
                return null;
            }

            var item = this.itemRepository.GetItemByStringId(stringId);

            if (item is not Character character)
            {
                return null;
            }

            this.templateCreator.Character = character;

            return this.templateCreator.Generate(data);
        }
    }
}
