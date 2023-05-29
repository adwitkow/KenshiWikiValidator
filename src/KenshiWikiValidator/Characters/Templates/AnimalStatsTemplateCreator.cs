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
    public class AnimalStatsTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Animal Stats";

        public AnimalCharacter? AnimalCharacter { get; internal set; }

        public WikiTemplate? Generate(ArticleData data)
        {
            if (this.AnimalCharacter is null)
            {
                throw new InvalidOperationException("Cannot create the template without an AnimalCharacter object");
            }

            var parameters = new IndexedDictionary<string, string?>()
            {
                /*
                 * TODO: Figure out whether these values are even needed or not
                 * { "melee attack", GetStatsValue(this.AnimalCharacter.Strength, 0) },
                 * { "dexterity", GetStatsValue(this.AnimalCharacter., 0) },
                 * { "toughness", GetStatsValue(this.AnimalCharacter., 0) },
                 */
                { "strength", GetStatsValue(this.AnimalCharacter.Strength, 0) },
                { "combat stats", GetStatsValue(this.AnimalCharacter.CombatStats, 0) },
                { "stats randomize", GetStatsValue(this.AnimalCharacter.StatsRandomise, 0) },
                { "lifespan", GetStatsValue(this.AnimalCharacter.Lifespan, 0) },
            };

            return new WikiTemplate(TemplateName, parameters);
        }

        private static string? GetStatsValue(float? value, float nullValue)
        {
            return value.GetValueOrDefault() == nullValue ? null : value.ToString();
        }
    }
}
