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
using KenshiWikiValidator.OcsProxy.Models.Interfaces;

namespace KenshiWikiValidator.WikiCategories.Characters.Templates
{
    public class CharacterStatsTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Character Stats";

        public IStatsContainer? StatsContainer { get; set; }

        public WikiTemplate? Generate(ArticleData data)
        {
            if (this.StatsContainer is null)
            {
                throw new InvalidOperationException("Cannot create a Stats template without an IStatsContainer object");
            }

            var parameters = new SortedList<string, string?>()
            {
                { "strength", GetStatsValue(this.StatsContainer.Strength, 0) },
                { "combat stats", GetStatsValue(this.StatsContainer.CombatStats, 0) },
                { "stealth stats", GetStatsValue(this.StatsContainer.StealthStats, 0) },
                { "unarmed stats", GetStatsValue(this.StatsContainer.UnarmedStats, 0) },
                { "ranged stats", GetStatsValue(this.StatsContainer.RangedStats, 0) },
                { "stats randomize", GetStatsValue(this.StatsContainer.StatsRandomise, 0) },
            };

            return new WikiTemplate(TemplateName, parameters);
        }

        private static string? GetStatsValue(float? value, float nullValue)
        {
            return value.GetValueOrDefault() == nullValue ? null : value.ToString();
        }
    }
}
