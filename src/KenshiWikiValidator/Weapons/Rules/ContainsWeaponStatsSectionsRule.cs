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
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.Weapons.Templates;

namespace KenshiWikiValidator.Weapons.Rules
{
    internal class ContainsWeaponStatsSectionsRule : ContainsSectionRuleBase
    {
        private static readonly IEnumerable<string> ManufacturerIds = new string[]
        {
            "912-gamedata.base",
            "917-gamedata.base",
            "1057-gamedata.base",
            "927-gamedata.base",
            "1070-gamedata.base",
            "52288-rebirth.mod",
        };

        private readonly IItemRepository itemRepository;

        public ContainsWeaponStatsSectionsRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().FirstOrDefault();

            if (stringId is null)
            {
                return null;
            }

            var item = this.itemRepository.GetItemByStringId(stringId);
            var weapon = item as Weapon;

            if (weapon is null)
            {
                return null;
            }

            var manufacturers = ManufacturerIds
                .Select(id => this.itemRepository.GetItemByStringId<WeaponManufacturer>(id));

            var builder = new WikiSectionBuilder()
                .WithHeader("Variations")
                .WithParagraph("All possible variations in an un-modded game, as of 1.0.55. Please note that the stats shown may differ slightly than in game as it tends to round values.");

            var templateCreator = new WeaponStatsTemplateCreator();

            var homemade = this.itemRepository.GetItemByStringId<WeaponManufacturer>("PLAYER_WEAPONS");
            var homemadeTemplates = templateCreator.Generate(weapon, homemade, data.WikiTemplates, true);

            if (homemadeTemplates.Any())
            {
                builder.WithSubsection("''Manufacturer''", 1);
            }

            foreach (var manufacturer in manufacturers)
            {
                var templates = templateCreator.Generate(weapon, manufacturer, data.WikiTemplates, false);

                foreach (var template in templates)
                {
                    builder.WithTemplate(template)
                        .WithNewline();
                }
            }

            if (homemadeTemplates.Any())
            {
                builder.WithSubsection("''Homemade''", 1);
                foreach (var template in homemadeTemplates)
                {
                    template.Parameters.TryGetValue("grade", out var grade);
                    if (string.IsNullOrEmpty(grade) || grade == "Edge Type 3" || grade == "Meitou")
                    {
                        continue;
                    }

                    builder.WithTemplate(template)
                        .WithNewline();
                }
            }

            var lastSection = data.Sections.LastOrDefault();
            if (lastSection == "Variations" || lastSection == "''Homemade''")
            {
                builder.WithTemplate(new WikiTemplate("Navbox/Weapons"))
                    .WithNewline();

                foreach (var category in data.Categories)
                {
                    builder.WithLine($"[[Category:{category}]]");
                }
            }

            return builder;
        }
    }
}
