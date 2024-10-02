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
    public class EquipmentSectionRule : ContainsSectionRuleBase
    {
        private const string AncientStringId = "917-gamedata.base";

        private static readonly List<(AttachSlot Slot, string Section)> SlotSections = new()
        {
            (AttachSlot.Hat, "Headgear"),
            (AttachSlot.Body, "Body"),
            (AttachSlot.Shirt, "Shirt"),
            (AttachSlot.Legs, "Legwear"),
            (AttachSlot.Boots, "Footwear"),
        };

        private readonly IWikiTitleCache titleCache;
        private readonly IItemRepository itemRepository;
        private readonly WeaponManufacturer ancient;

        public EquipmentSectionRule(IItemRepository itemRepository, IWikiTitleCache titleCache)
        {
            this.titleCache = titleCache;
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
            this.AddApparelSection(character, builder);
            this.AddInventorySection(character, builder);

            return builder;
        }

        private void AddInventorySection(Character character, WikiSectionBuilder builder)
        {
            var validBackpacks = character.Backpack.Where(item => item.Value0 > 0);
            var validInventory = character.Inventory.Where(item => item.Value0 > 0);

            if (validBackpacks.Any() || validInventory.Any())
            {
                builder.WithNewline();
                builder.WithSubsection("Inventory", 1);
            }

            if (validBackpacks.Any())
            {
                builder.WithEmptyTemplate("Loot Table Header");

                foreach (var backpack in validBackpacks)
                {
                    var template = new WikiTemplate("Loot Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    var name = this.titleCache.GetTitle(backpack.Item);
                    template.UnnamedParameters.Add(name);
                    template.UnnamedParameters.Add(backpack.Value0.ToString());

                    builder.WithTemplate(template);
                }

                builder.WithEmptyTemplate("Loot Table Bottom");

                if (validInventory.Any())
                {
                    builder.WithNewline();
                }
            }

            if (validInventory.Any())
            {
                builder.WithEmptyTemplate("Loot Table Header");

                foreach (var item in validInventory)
                {
                    var template = new WikiTemplate("Loot Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    var name = this.titleCache.GetTitle(item.Item);
                    template.UnnamedParameters.Add(name);
                    template.UnnamedParameters.Add(item.Value0.ToString());

                    builder.WithTemplate(template);
                }

                builder.WithEmptyTemplate("Loot Table Bottom");
            }
        }

        private void AddApparelSection(Character character, WikiSectionBuilder builder)
        {
            var validClothing = character.Clothing.Where(clothing => clothing.Value0 != 0);
            var negativeQuantityClothing = validClothing.Where(clothing => clothing.Value0 < 0);
            validClothing = validClothing.Except(negativeQuantityClothing);
            var clothingChances = new List<(string Section, List<ChanceItem> Chances)>();
            foreach (var slotSection in SlotSections)
            {
                var sectionChances = new List<ChanceItem>();
                var slot = slotSection.Slot;
                var noItemWeight = negativeQuantityClothing.Where(item => item.Item.Slot == slot)
                    .Sum(item => item.Value1);
                var items = validClothing.Where(item => item.Item.Slot == slot);
                var entireWeight = items.Sum(item => item.Value1) + noItemWeight;

                foreach (var item in items)
                {
                    var chance = (double)item.Value1 / entireWeight;
                    chance *= 100;
                    chance = Math.Round(chance, 2);

                    var name = this.titleCache.GetTitle(item.Item);
                    sectionChances.Add(new ChanceItem(name, chance));
                }

                clothingChances.Add((slotSection.Section, sectionChances));
            }

            builder.WithNewline();
            builder.WithSubsection("Apparel", 1);

            var nonEmptyChances = clothingChances.Where(slot => slot.Chances.Count > 0);
            if (!nonEmptyChances.Any())
            {
                builder.WithLine("''This character spawns with no apparel.''");
                return;
            }

            var hasGradedArmour = validClothing.Any(r => r.Item.PartCoverage.Any());
            if (hasGradedArmour)
            {
                builder.WithEmptyTemplate("Grade Table Header");

                var gradeRowTemplate = new WikiTemplate("Grade Table Row")
                {
                    Format = WikiTemplate.TemplateFormat.Inline,
                };

                var upgradeChance = character.ArmourUpgradeChance.GetValueOrDefault();
                if (character.ArmourGrade == ArmourGrade.Masterwork)
                {
                    upgradeChance = 0;
                }

                gradeRowTemplate.UnnamedParameters.Add(character.ArmourGrade.ToString());
                gradeRowTemplate.UnnamedParameters.Add((100 - upgradeChance).ToString());

                builder.WithTemplate(gradeRowTemplate);

                if (upgradeChance > 0)
                {
                    var gradeUpgradeRowTemplate = new WikiTemplate("Grade Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    gradeUpgradeRowTemplate.UnnamedParameters.Add((character.ArmourGrade + 1).ToString());
                    gradeUpgradeRowTemplate.UnnamedParameters.Add(upgradeChance.ToString());

                    builder.WithTemplate(gradeUpgradeRowTemplate);
                }

                builder.WithEmptyTemplate("Grade Table Bottom");
            }
            else
            {
                builder.WithLine("''Every piece of clothing below offers no coverage and thus, armour grading is not applicable.''");
            }

            foreach (var slotSection in nonEmptyChances)
            {
                builder.WithNewline();
                if (nonEmptyChances.Count() > 1)
                {
                    builder.WithSubsection(slotSection.Section, 2);
                }

                builder.WithEmptyTemplate("Equipment Table Header");

                var orderedChances = slotSection.Chances.OrderByDescending(chance => chance.Chance);
                foreach (var chance in orderedChances)
                {
                    var template = new WikiTemplate("Equipment Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    template.UnnamedParameters.Add(chance.ItemName);
                    template.UnnamedParameters.Add(chance.Chance.ToString());

                    builder.WithTemplate(template);
                }

                builder.WithEmptyTemplate("Equipment Table Bottom");
            }
        }

        private void AddWeaponSection(Character character, WikiSectionBuilder builder)
        {
            var validWeapons = GatherValidWeapons(character);

            var hipReferences = validWeapons.Where(r => r.Value1 == 0);
            var tooBigForHip = hipReferences.Where(r => !r.Item.FitsOnHip);
            hipReferences = hipReferences.Except(tooBigForHip);

            var invalidSlotReferences = validWeapons.Where(r => r.Value1 is not 0 or 1);
            var weaponsMeantForBack = validWeapons.Where(r => r.Value1 == 1);
            var backReferences = tooBigForHip
                .Concat(weaponsMeantForBack)
                .Concat(invalidSlotReferences);

            var crossbowChances = this.CalculateWeaponChances(character.Crossbows, r => r.Value1);
            var crossbowChanceSum = crossbowChances.Sum(x => x.Chance);
            var hipChances = this.CalculateWeaponChances(hipReferences, r => r.Value2)
                .OrderByDescending(x => x.Chance);
            var rawBackChances = this.CalculateWeaponChances(backReferences, r => r.Value2);
            var crossbowSubtraction = crossbowChanceSum / rawBackChances.Count();
            var backChances = rawBackChances.Select(x => new ChanceItem(x.ItemName, x.Chance - crossbowSubtraction))
                .Concat(crossbowChances)
                .OrderByDescending(x => x.Chance);

            builder.WithNewline();
            builder.WithSubsection("Weapons", 1);

            if (!backChances.Any() && !hipChances.Any())
            {
                builder.WithLine("''This character spawns with no weapons.''");
                return;
            }

            var manufacturerChances = this.CalculateManufacturerChances(character);
            var headerTemplate = new WikiTemplate("Grade Table Header");
            headerTemplate.UnnamedParameters.Add("weapon");

            builder.WithTemplate(headerTemplate);

            foreach (var manufacturerChance in manufacturerChances)
            {
                var rowTemplate = new WikiTemplate("Grade Table Row")
                {
                    Format = WikiTemplate.TemplateFormat.Inline,
                };

                rowTemplate.UnnamedParameters.Add("weapon");
                rowTemplate.UnnamedParameters.Add(manufacturerChance.ItemName);
                rowTemplate.UnnamedParameters.Add(manufacturerChance.Chance.ToString());

                builder.WithTemplate(rowTemplate);
            }

            builder.WithEmptyTemplate("Grade Table Bottom");

            if (backChances.Any())
            {
                builder.WithNewline();
                builder.WithSubsection("Back slot", 2);
                builder.WithEmptyTemplate("Equipment Table Header");

                foreach (var chance in backChances)
                {
                    var template = new WikiTemplate("Equipment Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    template.UnnamedParameters.Add(chance.ItemName);
                    template.UnnamedParameters.Add(chance.Chance.ToString());

                    builder.WithTemplate(template);
                }

                builder.WithEmptyTemplate("Equipment Table Bottom");
            }

            if (hipChances.Any())
            {
                builder.WithNewline();
                builder.WithSubsection("Hip slot", 2);

                builder.WithEmptyTemplate("Equipment Table Header");

                foreach (var chance in hipChances)
                {
                    var template = new WikiTemplate("Equipment Table Row")
                    {
                        Format = WikiTemplate.TemplateFormat.Inline,
                    };

                    template.UnnamedParameters.Add(chance.ItemName);
                    template.UnnamedParameters.Add(chance.Chance.ToString());

                    builder.WithTemplate(template);
                }

                builder.WithEmptyTemplate("Equipment Table Bottom");
            }
        }

        private List<ChanceItem> CalculateManufacturerChances(Character character)
        {
            var results = new List<ChanceItem>();
            var manufacturers = character.WeaponLevels;

            if (!manufacturers.Any())
            {
                manufacturers = new List<ItemReference<WeaponManufacturer>>()
                {
                    new ItemReference<WeaponManufacturer>(this.ancient),
                };
            }

            manufacturers = manufacturers.Select(m =>
            {
                var v0 = m.Value0 == 0 ? 100 : m.Value0;
                return new ItemReference<WeaponManufacturer>(m.Item, v0, 0, 0);
            });

            var manufacturerWeightSum = manufacturers.Sum(r => r.Value0);
            var orderedManufacturers = manufacturers.OrderBy(r => r.Item.WeaponModels.Min(w => w.Value0));
            foreach (var manufacturer in orderedManufacturers)
            {
                var manufacturerChance = (double)manufacturer.Value0 / manufacturerWeightSum;
                manufacturerChance = Math.Round(manufacturerChance * 100, 2);

                var result = new ChanceItem(manufacturer.Item.Name, manufacturerChance);
                results.Add(result);
            }

            return results;
        }

        private IEnumerable<ChanceItem> CalculateWeaponChances<T>(
            IEnumerable<ItemReference<T>> references,
            Func<ItemReference<T>, int> chanceProperty)
            where T : IItem
        {
            var results = new List<ChanceItem>();
            var chanceSum = 0;

            // TODO: This is duplicating the work performed in GatherValidWeapons method
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
                    var name = this.titleCache.GetTitle(weapon.Item);
                    results.Add(new ChanceItem(name, chance));
                }

                if (chanceSum >= 100)
                {
                    break;
                }
            }

            return results;
        }

        private static IEnumerable<ItemReference<Weapon>> GatherValidWeapons(Character character)
        {
            var results = new List<ItemReference<Weapon>>();
            var validWeapons = character.Weapons.Where(r => r.Value0 > 0);

            var hipWeapons = validWeapons.Where(r => r.Value1 == 0);
            var backWeapons = validWeapons.Where(r => r.Value1 == 1);
            var remainingWeapons = validWeapons.Where(r => r.Value1 is not 0 or 1);

            var weaponGroups = new[] { hipWeapons, backWeapons, remainingWeapons };

            foreach (var weaponGroup in weaponGroups)
            {
                var chanceSum = 0;

                foreach (var reference in weaponGroup)
                {
                    var chance = reference.Value2;
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

                    var result = new ItemReference<Weapon>(
                        reference.Item,
                        reference.Value0,
                        reference.Value1,
                        chance);
                    results.Add(result);

                    if (chanceSum >= 100)
                    {
                        break;
                    }
                }
            }

            return results;
        }

        private readonly struct ChanceItem(string itemName, double chance)
        {
            public string ItemName => itemName;

            public double Chance => chance;
        }
    }
}
