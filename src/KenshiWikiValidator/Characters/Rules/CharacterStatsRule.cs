// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using KenshiWikiValidator.WikiCategories.Characters.Templates;

namespace KenshiWikiValidator.WikiCategories.Characters.Rules
{
    public class CharacterStatsRule : ContainsDetailedTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly StatsTemplateCreator statsTemplateCreator;
        private readonly AnimalStatsTemplateCreator animalStatsTemplateCreator;

        public CharacterStatsRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.statsTemplateCreator = new StatsTemplateCreator(itemRepository);
            this.animalStatsTemplateCreator = new AnimalStatsTemplateCreator();
        }

        protected override WikiTemplate? PrepareTemplate(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().SingleOrDefault();

            if (string.IsNullOrEmpty(stringId))
            {
                return null;
            }

            bool isAnimal = false;
            IStatsContainer item;
            if (data.Categories.Contains("Lore"))
            {
                return null;
            }
            else if (data.Categories.Contains("Animals"))
            {
                item = this.itemRepository.GetItemByStringId<AnimalCharacter>(stringId);
                isAnimal = true;
            }
            else
            {
                item = this.itemRepository.GetItemByStringId<Character>(stringId);
            }

            ITemplateCreator creator;
            if (isAnimal)
            {
                this.animalStatsTemplateCreator.AnimalCharacter = item as AnimalCharacter;
                creator = this.animalStatsTemplateCreator;
            }
            else
            {
                this.statsTemplateCreator.Character = item as Character;
                creator = this.statsTemplateCreator;
            }

            return creator.Generate(data);
        }
    }
}
