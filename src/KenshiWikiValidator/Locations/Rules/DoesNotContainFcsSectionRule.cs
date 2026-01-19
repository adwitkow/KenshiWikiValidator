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

namespace KenshiWikiValidator.Locations.Rules
{
    public partial class ShouldNotContainFcsSectionRule : IValidationRule
    {
        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var matches = FcsSectionRegex().Matches(content);
            foreach (Match match in matches)
            {
                result.AddIssue(match.Value);
            }

            if (!result.Success)
            {
                this.PrintToFile(title, result.Issues);
            }

            return result;
        }

        [GeneratedRegex("==.+fcs.+==", RegexOptions.IgnoreCase)]
        private static partial Regex FcsSectionRegex();

        private void PrintToFile(string title, IEnumerable<string> issues)
        {
            var output = Path.Combine("output", "sections");
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            var file = Path.Combine(output, $"{nameof(ShouldNotContainFcsSectionRule)}.txt");

            var contents = string.Join("; ", issues);
            var line = $"{title}: {contents}";
            File.AppendAllLines(file, [ line ]);
        }
    }
}
