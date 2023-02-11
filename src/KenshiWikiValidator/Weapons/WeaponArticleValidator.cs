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
using KenshiWikiValidator.TownResidents;
using KenshiWikiValidator.Weapons.Rules;

namespace KenshiWikiValidator.Weapons
{
    public class WeaponArticleValidator : ArticleValidatorBase
    {
        private readonly List<IArticleValidator> dependencies;
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles, TownResidentArticleValidator townResidentValidator)
            : base(itemRepository, wikiTitles)
        {
            this.dependencies = new List<IArticleValidator>() { townResidentValidator };
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Weapon", new[] { "Weapon Types" }),
                new NewLinesRule(),
                new ContainsBlueprintsSectionRule(itemRepository, wikiTitles),
                new ContainsWeaponTemplateRule(itemRepository),
                new ContainsWeaponNavboxRule(),
                new ContainsWeaponCraftingSectionRule(itemRepository),
                new WeaponPriceScraper(),
            };
        }

        public override string CategoryName => "Weapons";

        public override IEnumerable<IValidationRule> Rules => this.rules;

        public override IEnumerable<IArticleValidator> Dependencies => this.dependencies;
    }
}