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
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.WikiCategories.Characters.Templates
{
    public class StatsTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Stats";

        public Stats? Stats { get; set; }

        public int? StatsRandomise { get; set; }

        public WikiTemplate? Generate(ArticleData data)
        {
            if (this.Stats is null)
            {
                throw new InvalidOperationException("Cannot create a Stats template without a Stats object");
            }

            var parameters = new IndexedDictionary<string, string?>()
            {
                { "Strength", GetStatsValue(this.Stats.Strength, 1) },
                { "Toughness", GetStatsValue(this.Stats.Toughness, 1) },
                { "Dexterity", GetStatsValue(this.Stats.Dexterity, 1) },
                { "Perception", GetStatsValue(this.Stats.Perception, 1) },
                { "Melee Attack", GetStatsValue(this.Stats.MeleeAttack, 1) },
                { "Melee Defence", GetStatsValue(this.Stats.MeleeDefence, 1) },
                { "Dodge", GetStatsValue(this.Stats.Dodge, 1) },
                { "Martial Arts", GetStatsValue(this.Stats.MartialArts, 1) },
                { "Katanas", GetStatsValue(this.Stats.Katana, 1) },
                { "Sabres", GetStatsValue(this.Stats.Sabres, 1) },
                { "Hackers", GetStatsValue(this.Stats.Hackers, 1) },
                { "Heavy Weapons", GetStatsValue(this.Stats.HeavyWeapons, 1) },
                { "Blunt", GetStatsValue(this.Stats.Blunt, 1) },
                { "Polearms", GetStatsValue(this.Stats.Polearms, 1) },
                { "Turrets", GetStatsValue(this.Stats.Turrets, 1) },
                { "Crossbows", GetStatsValue(this.Stats.Crossbow, 1) },
                { "Precision Shooting", GetStatsValue(this.Stats.PrecisionShooting, 1) },
                { "Stealth", GetStatsValue(this.Stats.Stealth, 1) },
                { "Lockpicking", GetStatsValue(this.Stats.Lockpicking, 1) },
                { "Thievery", GetStatsValue(this.Stats.Thievery, 1) },
                { "Assassination", GetStatsValue(this.Stats.Assassination, 1) },
                { "Athletics", GetStatsValue(this.Stats.Athletics, 1) },
                { "Swimming", GetStatsValue(this.Stats.Swimming, 1) },
                { "Field Medic", GetStatsValue(this.Stats.FieldMedic, 1) },
                { "Engineer", GetStatsValue(this.Stats.Engineer, 1) },
                { "Robotics", GetStatsValue(this.Stats.Robotics, 1) },
                { "Science", GetStatsValue(this.Stats.Science, 1) },
                { "Weapon Smith", GetStatsValue(this.Stats.WeaponSmith, 1) },
                { "Armour Smith", GetStatsValue(this.Stats.ArmourSmith, 1) },
                { "Crossbow Smith", GetStatsValue(this.Stats.CrossbowSmith, 1) },
                { "Labouring", GetStatsValue(this.Stats.Labouring, 1) },
                { "Farming", GetStatsValue(this.Stats.Farming, 1) },
                { "Cooking", GetStatsValue(this.Stats.Cooking, 1) },
                { "stats randomize", GetStatsValue(this.StatsRandomise, 0) },
            };

            return new WikiTemplate(TemplateName, parameters);
        }

        private static string? GetStatsValue(float? value, float nullValue)
        {
            return value.GetValueOrDefault() == nullValue ? null : value.ToString();
        }
    }
}
