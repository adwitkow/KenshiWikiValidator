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

namespace KenshiWikiValidator.Armours.Rules
{
    [ExcludeFromCodeCoverage]
    public class ArmourPriceScraper : ScrapingRuleBase
    {
        protected override string FileName => "prices";

        protected override string[] GetLines(string title, string content, ArticleData data)
        {
            var statsTemplates = data.WikiTemplates.Where(template => template.Name == "Armour");

            if (!statsTemplates.Any())
            {
                return Array.Empty<string>();
            }

            var results = new List<string>
            {
                $"    [\"{title}\"] = {{",
            };

            foreach (var parameters in statsTemplates.Select(template => template.Parameters))
            {
                var grade = parameters["Grade"];
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
