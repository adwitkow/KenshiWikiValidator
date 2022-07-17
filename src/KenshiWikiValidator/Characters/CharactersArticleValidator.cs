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
using KenshiWikiValidator.BaseComponents.SharedRules;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.Characters.Rules;

namespace KenshiWikiValidator.Characters
{
    public class CharactersArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public CharactersArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
            : base(itemRepository, wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Character"),
                new CharacterStatsRule(itemRepository),
            };
        }

        public override string CategoryName => "Characters";

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
