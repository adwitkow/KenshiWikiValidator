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

using System.Text.RegularExpressions;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.WikiTemplates.Creators
{
    public class WeaponTemplateCreator : ITemplateCreator
    {
        private const string WikiTemplateName = "Weapon";

        private readonly IItemRepository itemRepository;
        private readonly ArticleData data;

        private readonly Dictionary<int, string> skillToClassMap;

        public WeaponTemplateCreator(IItemRepository itemRepository, ArticleData data)
        {
            this.itemRepository = itemRepository;
            this.data = data;

            this.skillToClassMap = new Dictionary<int, string>()
            {
                { 0, "Katana" },
                { 1, "Sabre" },
                { 2, "Blunt weapon" },
                { 3, "Heavy weapons" },
                { 4, "Hacker" },
                { 8, "Polearm" },
            };
        }

        public WikiTemplate? Generate()
        {
            var stringId = this.data.StringIds.SingleOrDefault();
            if (string.IsNullOrEmpty(stringId))
            {
                return null;
            }

            var item = this.itemRepository.GetItemByStringId<Weapon>(stringId);

            var armourPenetration = FormatArmourPenetration(item.ArmourPenetration);
            var bloodLoss = FormatMultiplier(item.BleedMultiplier);
            var attack = FormatIntStat(item.AttackModifier);
            var defence = FormatIntStat(item.DefenceModifier);
            var indoors = FormatIntStat(item.IndoorsModifier);
            var robotDamage = FormatMultiplier(item.RobotDamageMultiplier, true);
            var humanDamage = FormatMultiplier(item.HumanDamageMulitplier, true);
            var animalDamage = FormatMultiplier(item.AnimalDamageMultiplier, true);

            var reach = item.Length.ToString();
            var description = item.Description;

            if (!string.IsNullOrEmpty(description))
            {
                description = Regex.Replace(description, @"\r\n|\r|\n", "<br />");
            }

            var skillCategory = item.SkillCategory.GetValueOrDefault();
            var weaponClass = this.skillToClassMap[skillCategory];

            string? spiderDamage = null;
            string? smallSpiderDamage = null;
            string? bonedogDamage = null;
            string? skimmerDamage = null;
            string? beakThingDamage = null;
            string? gorilloDamage = null;
            string? leviathanDamage = null;

            var raceDamageReferences = item.RaceDamage;
            foreach (var reference in raceDamageReferences)
            {
                var name = reference.Item.Name;

                var damage = FormatIntStat(reference.Value0 - 100);

                switch (name)
                {
                    case "Spider":
                        spiderDamage = damage;
                        break;
                    case "Small Spider":
                        smallSpiderDamage = damage;
                        break;
                    case "Bonedog":
                        bonedogDamage = damage;
                        break;
                    case "Skimmer":
                        skimmerDamage = damage;
                        break;
                    case "Beak Thing":
                        beakThingDamage = damage;
                        break;
                    case "Gorillo":
                        gorilloDamage = damage;
                        break;
                    case "Leviathan":
                        leviathanDamage = damage;
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected race damage: " + name);
                }
            }

            var properties = new SortedList<string, string?>()
            {
                { "class", weaponClass },
                { "blood loss", bloodLoss },
                { "armour penetration", armourPenetration },
                { "attack", attack },
                { "defence", defence },
                { "indoors", indoors },
                { "damage_robots", robotDamage },
                { "damage_humans", humanDamage },
                { "damage_animals", animalDamage },
                { "damage_spider", spiderDamage },
                { "damage_small spider", smallSpiderDamage },
                { "damage_bonedog", bonedogDamage },
                { "damage_skimmer", skimmerDamage },
                { "damage_beak thing", beakThingDamage },
                { "damage_gorillo", gorilloDamage },
                { "damage_leviathan", leviathanDamage },
                { "reach", reach },
                { "description", description },
                { "string id", stringId },
            };

            return new WikiTemplate(WikiTemplateName, properties);
        }

        private static string? FormatArmourPenetration(float? input)
        {
            return FormatMultiplier(input + 1, true);
        }

        private static string? FormatMultiplier(float? input, bool isPercentage = false)
        {
            if (!input.HasValue || input == 1f)
            {
                return null;
            }

            if (isPercentage)
            {
                input = (input - 1) * 100f;
                return FormatIntStat((int)input);
            }

            return input.Value.ToString("F2");
        }

        private static string? FormatIntStat(int? input)
        {
            if (!input.HasValue || input == 0)
            {
                return null;
            }

            string result;
            if (input > 0)
            {
                result = $"+{input}";
            }
            else
            {
                result = input.Value.ToString();
            }

            return result;
        }
    }
}
