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
