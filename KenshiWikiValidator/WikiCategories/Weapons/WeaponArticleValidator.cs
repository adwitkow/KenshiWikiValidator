﻿// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
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