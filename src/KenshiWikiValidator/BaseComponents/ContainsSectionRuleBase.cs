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

namespace KenshiWikiValidator.BaseComponents
{
    public abstract class ContainsSectionRuleBase : IValidationRule
    {
        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var sectionBuilder = this.CreateSectionBuilder(data);

            if (sectionBuilder is null)
            {
                return result;
            }

            var section = sectionBuilder.WikiSection;
            var sectionContent = sectionBuilder.Build();

            var output = Path.Combine("output", "sections", this.GetType().Name);
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            var contentToValidate = MakeNewlinesConsistent(content);

            if (!contentToValidate.Contains(sectionContent))
            {
                result.AddIssue($"Incorrect or missing '{section.Header}' section");

                var sectionPath = Path.Combine(output, $"{title.Replace('/', ' ')}-{section.Header}-Section.txt");
                File.WriteAllText(sectionPath, sectionContent);
            }

            return result;
        }

        protected abstract WikiSectionBuilder? CreateSectionBuilder(ArticleData data);

        private static string MakeNewlinesConsistent(string input)
        {
            return Regex.Replace(input, @"\r\n|\r|\n", Environment.NewLine);
        }
    }
}
