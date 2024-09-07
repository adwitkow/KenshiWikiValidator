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

namespace KenshiWikiValidator.Characters.Rules
{
    internal class EquipmentSectionRule : ContainsSectionRuleBase
    {
        private const string AncientStringId = "917-gamedata.base";

        private readonly IItemRepository itemRepository;
        private readonly WeaponManufacturer ancient;

        public EquipmentSectionRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
            this.ancient = itemRepository.GetItemByStringId<WeaponManufacturer>(AncientStringId);
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().FirstOrDefault();

            if (string.IsNullOrEmpty(stringId))
            {
                return null;
            }

            var character = this.itemRepository.GetItemByStringId<Character>(stringId);

            if (character is null)
            {
                return null;
            }

            var builder = new WikiSectionBuilder();
            builder.WithHeader("Equipment");
            this.AddWeaponSection(character, builder);

            //builder.WithSubsection("Clothing", 1);
            //builder.WithSubsection("Inventory", 1);

            return builder;
        }

        private void AddWeaponSection(Character character, WikiSectionBuilder builder)
        {
            var crossbows = ExtractWeaponReferences(character.Crossbows, r => r.Value1);

            var validWeapons = character.Weapons.Where(r => r.Value0 > 0);
            var validBackWeapons = validWeapons.Where(r => r.Value1 == 1);
            var backWeapons = ExtractWeaponReferences(validBackWeapons, r => r.Value2);
            var validHipWeapons = validWeapons.Where(r => r.Value1 != 1);
            var hipWeapons = ExtractWeaponReferences(validHipWeapons, r => r.Value2);

            if (!crossbows.Any() && !validWeapons.Any())
            {
                return;
            }

            builder.WithSubsection("Weapons", 1);

            builder.WithSubsection("Grades", 2);

            var grades = this.UnwrapWeaponGrades(character, builder);
            builder.WithLine("{| class=\"wikitable sortable\" style=\"text-align: center;\"");
            builder.WithLine("! Manufacturer !! Model !! Chance");

            var groupedGrades = grades.GroupBy(g => g.Manufacturer);
            foreach (var group in groupedGrades)
            {
                bool addManufacturer = true;
                foreach (var model in group)
                {
                    builder.WithLine("|-");

                    if (addManufacturer)
                    {
                        builder.WithLine($"| rowspan=\"{group.Count()}\" | [[{model.Manufacturer}]]");
                    }

                    builder.WithLine($"| {{{{Grade|{model.Model}}}}}");
                    builder.WithLine($"| {model.Chance:0.##}%");

                    addManufacturer = false;
                }
            }

            builder.WithLine("|}");
            builder.WithNewline();

            if (crossbows.Any() || hipWeapons.Any(weapon => !((Weapon)weapon.Item).FitsOnHip))
            {
                builder.WithLine("Edge case");
                return;
            }

            if (crossbows.Any())
            {
                builder.WithSubsection("Ranged", 2);
                builder.WithLine("{| class=\"wikitable sortable\" style=\"text-align: center;\"");
                builder.WithLine("! colspan=\"2\" | Name !! Chance");

                foreach (var crossbow in crossbows)
                {
                    builder.WithLine("|-");
                    builder.WithLine($"| [[File:{crossbow.Item.Name}.png | 200px]]");
                    builder.WithLine($"| [[{crossbow.Item.Name}]]");
                    builder.WithLine($"| {crossbow.Chance}%");
                }

                builder.WithLine("|}");
                builder.WithNewline();
            }

            var maxGrade = grades.Last().Model;

            if (backWeapons.Any())
            {
                builder.WithSubsection("Back slot", 2);
                builder.WithLine("{| class=\"wikitable sortable\" style=\"text-align: center;\"");
                builder.WithLine("! colspan=\"2\" | Name !! Chance");

                foreach (var backWeapon in backWeapons)
                {
                    builder.WithLine("|-");
                    builder.WithLine($"| [[File:{maxGrade} {backWeapon.Item.Name}.png | 200px]]");
                    builder.WithLine($"| [[{backWeapon.Item.Name}]]");
                    builder.WithLine($"| {backWeapon.Chance}%");
                }

                builder.WithLine("|}");
                builder.WithNewline();
            }

            if (hipWeapons.Any())
            {
                builder.WithSubsection("Hip slot", 2);
                builder.WithLine("{| class=\"wikitable sortable\" style=\"text-align: center;\"");
                builder.WithLine("! colspan=\"2\" | Name !! Chance");

                foreach (var hipWeapon in hipWeapons)
                {
                    builder.WithLine("|-");
                    builder.WithLine($"| [[File:{maxGrade} {hipWeapon.Item.Name}.png | 200px]]");
                    builder.WithLine($"| [[{hipWeapon.Item.Name}]]");
                    builder.WithLine($"| {hipWeapon.Chance}%");
                }

                builder.WithLine("|}");
                builder.WithNewline();
            }
        }

        private List<ModelChance> UnwrapWeaponGrades(Character character, WikiSectionBuilder builder)
        {
            var results = new List<ModelChance>();
            var manufacturers = character.WeaponLevels;

            if (!manufacturers.Any())
            {
                manufacturers = new List<ItemReference<WeaponManufacturer>>()
                {
                    new ItemReference<WeaponManufacturer>(this.ancient),
                };
            }

            manufacturers = manufacturers
                .Select(m =>
                {
                    var v0 = m.Value0 == 0 ? 100 : m.Value0;
                    return new ItemReference<WeaponManufacturer>(m.Item, v0, 0, 0);
                });

            var manufacturerWeightSum = manufacturers.Sum(r => r.Value0);
            var orderedManufacturers = manufacturers.OrderBy(r => r.Item.WeaponModels.Min(w => w.Value0));
            foreach (var manufacturer in orderedManufacturers)
            {
                var manufacturerChance = (double)manufacturer.Value0 / manufacturerWeightSum;

                var models = manufacturer.Item.WeaponModels.OrderBy(m => m.Value0);
                var modelWeightSum = models.Sum(r => r.Value1);
                foreach (var model in models)
                {
                    var modelChance = (double)model.Value1 / modelWeightSum;
                    modelChance *= manufacturerChance;
                    modelChance *= 100;

                    var result = new ModelChance(
                        manufacturer.Item.Name,
                        model.Item.Name,
                        Math.Round(modelChance, 2));
                    results.Add(result);
                }
            }

            return results;
        }

        private static List<(IItem Item, int Chance)> ExtractWeaponReferences<T>(
            IEnumerable<ItemReference<T>> references,
            Func<ItemReference<T>, int> chanceProperty)
            where T : IItem
        {
            var results = new List<(IItem item, int Chance)>();
            var chanceSum = 0;
            foreach (var weapon in references)
            {
                var chance = chanceProperty(weapon);
                if (chance == 0)
                {
                    chance = 100;
                }

                var candidate = chanceSum + chance;

                if (candidate > 100)
                {
                    chance = 100 - chanceSum;
                }

                chanceSum += chance;

                var amount = weapon.Value0;

                for (int i = 0; i < amount; i++)
                {
                    results.Add((weapon.Item, chance));
                }

                if (chanceSum >= 100)
                {
                    break;
                }
            }

            return results;
        }

        private readonly struct ModelChance(
            string manufacturer,
            string model,
            double chance)
        {
            public string Manufacturer => manufacturer;

            public string Model => model;

            public double Chance => chance;
        }
    }
}
