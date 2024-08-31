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
using KenshiWikiValidator.Characters;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.WikiCategories.Characters.Templates
{
    public class StatsTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Stats";

        private readonly CharacterRaceExtractor raceExtractor;

        public StatsTemplateCreator(IItemRepository itemRepository)
        {
            this.raceExtractor = new CharacterRaceExtractor(itemRepository);
        }

        public Character? Character { get; set; }

        public WikiTemplate? Generate(ArticleData data)
        {
            if (this.Character is null)
            {
                throw new InvalidOperationException("Cannot create a Stats template without a Character object");
            }

            IndexedDictionary<string, string?> parameters;
            bool calculated;
            if (this.Character.Stats.Any())
            {
                calculated = false;

                var stats = this.Character.Stats.Single().Item;
                parameters = this.GenerateParamsFromStatsObject(stats, this.Character.StatsRandomise);
            }
            else
            {
                calculated = true;
                parameters = this.GenerateParamsFromCharacterObject(this.Character);
            }

            string[] unnamedParams;
            if (calculated)
            {
                unnamedParams = ["calculated"];
            }
            else
            {
                unnamedParams = Array.Empty<string>();
            }

            var races = this.raceExtractor.Extract(this.Character);
            var raceNames = races.Select(race => race.Name);
            parameters.Insert(0, "Races", string.Join(",", raceNames));

            return new WikiTemplate(TemplateName, unnamedParams, parameters);
        }

        private static string? GetStatsValue(float? value, float nullValue)
        {
            return value.GetValueOrDefault() == nullValue ? null : value.ToString();
        }

        private IndexedDictionary<string, string?> GenerateParamsFromStatsObject(Stats stats, int? randomise)
        {
            return new IndexedDictionary<string, string?>()
            {
                { "Strength", GetStatsValue(stats.Strength, 1) },
                { "Toughness", GetStatsValue(stats.Toughness, 1) },
                { "Dexterity", GetStatsValue(stats.Dexterity, 1) },
                { "Perception", GetStatsValue(stats.Perception, 1) },
                { "Melee Attack", GetStatsValue(stats.MeleeAttack, 1) },
                { "Melee Defence", GetStatsValue(stats.MeleeDefence, 1) },
                { "Dodge", GetStatsValue(stats.Dodge, 1) },
                { "Martial Arts", GetStatsValue(stats.MartialArts, 1) },
                { "Katanas", GetStatsValue(stats.Katana, 1) },
                { "Sabres", GetStatsValue(stats.Sabres, 1) },
                { "Hackers", GetStatsValue(stats.Hackers, 1) },
                { "Heavy Weapons", GetStatsValue(stats.HeavyWeapons, 1) },
                { "Blunt", GetStatsValue(stats.Blunt, 1) },
                { "Polearms", GetStatsValue(stats.Polearms, 1) },
                { "Turrets", GetStatsValue(stats.Turrets, 1) },
                { "Crossbows", GetStatsValue(stats.Crossbow, 1) },
                { "Precision Shooting", GetStatsValue(stats.PrecisionShooting, 1) },
                { "Stealth", GetStatsValue(stats.Stealth, 1) },
                { "Lockpicking", GetStatsValue(stats.Lockpicking, 1) },
                { "Thievery", GetStatsValue(stats.Thievery, 1) },
                { "Assassination", GetStatsValue(stats.Assassination, 1) },
                { "Athletics", GetStatsValue(stats.Athletics, 1) },
                { "Swimming", GetStatsValue(stats.Swimming, 1) },
                { "Field Medic", GetStatsValue(stats.FieldMedic, 1) },
                { "Engineer", GetStatsValue(stats.Engineer, 1) },
                { "Robotics", GetStatsValue(stats.Robotics, 1) },
                { "Science", GetStatsValue(stats.Science, 1) },
                { "Weapon Smith", GetStatsValue(stats.WeaponSmith, 1) },
                { "Armour Smith", GetStatsValue(stats.ArmourSmith, 1) },
                { "Crossbow Smith", GetStatsValue(stats.CrossbowSmith, 1) },
                { "Labouring", GetStatsValue(stats.Labouring, 1) },
                { "Farming", GetStatsValue(stats.Farming, 1) },
                { "Cooking", GetStatsValue(stats.Cooking, 1) },
                { "stats randomize", GetStatsValue(randomise, 0) },
            };
        }

        private IndexedDictionary<string, string?> GenerateParamsFromCharacterObject(Character character)
        {
            return new IndexedDictionary<string, string?>()
            {
                { "strength", GetStatsValue(character.Strength, 0) },
                { "combat stats", GetStatsValue(character.CombatStats, 0) },
                { "stealth stats", GetStatsValue(character.StealthStats, 0) },
                { "unarmed stats", GetStatsValue(character.UnarmedStats, 0) },
                { "ranged stats", GetStatsValue(character.RangedStats, 0) },
                { "stats randomize", GetStatsValue(character.StatsRandomise, 0) },
            };
        }
    }
}
