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

using System.Diagnostics.CodeAnalysis;
using KenshiWikiValidator.BaseComponents;

namespace KenshiWikiValidator.Weapons.Rules
{
    [ExcludeFromCodeCoverage]
    public class WeaponPriceScraper : ScrapingRuleBase
    {
        protected override string FileName => "prices";

        protected override string[] GetLines(string title, string content, ArticleData data)
        {
            var statsTemplates = data.WikiTemplates.Where(template => template.Name == "WeaponStats");

            if (!statsTemplates.Any())
            {
                return Array.Empty<string>();
            }

            statsTemplates = statsTemplates.DistinctBy(template => template.Parameters["grade"]);

            var results = new List<string>
            {
                $"    [\"{title}\"] = {{",
            };

            foreach (var parameters in statsTemplates.Select(template => template.Parameters))
            {
                var grade = parameters["grade"];
                var value = parameters["value"];

                if (grade is null || value is null)
                {
                    throw new InvalidOperationException($"Either grade or value is null in '{title}' article.");
                }

                results.Add($"        [\"{grade}\"] = {value.Replace(",", string.Empty)},");
            }

            results.Add("    },");

            return results.ToArray();
        }
    }
}
