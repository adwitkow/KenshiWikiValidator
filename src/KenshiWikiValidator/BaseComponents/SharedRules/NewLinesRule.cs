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

namespace KenshiWikiValidator.BaseComponents.SharedRules
{
    public class NewLinesRule : IValidationRule
    {
        private static readonly Regex FooterRegex = new Regex(@"\[\[(Category|ru):(.*?)\]\]");

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();
            using var reader = new StringReader(content);
            var line = reader.ReadLine();

            var firstLine = true;
            var previousLine = string.Empty;
            while (line != null)
            {
                line = HandleIgnores(result, reader, line);

                if (line is null)
                {
                    break;
                }

                line = HandleTemplates(result, reader, line);
                line = HandleTables(result, reader, line);

                if (!firstLine)
                {
                    HandleNewlines(result, line, previousLine);
                }

                firstLine = false;
                previousLine = line;
                line = reader.ReadLine();
            }

            return result;
        }

        private static string? HandleIgnores(RuleResult result, StringReader reader, string? line)
        {
            line = HandleMarkup("gallery", result, reader, line!);
            line = HandleMarkup("tabview", result, reader, line!);

            var matches = FooterRegex.Matches(line.Trim());

            if (matches.Any())
            {
                while (line is not null)
                {
                    matches = FooterRegex.Matches(line.Trim());

                    if (matches.Count > 1)
                    {
                        result.AddIssue("Not enough newlines among categories/language links");
                    }

                    if (string.IsNullOrEmpty(line))
                    {
                        result.AddIssue("Empty lines among categories/language links");
                    }
                    else if (!matches.Any())
                    {
                        result.AddIssue("Footer consists of things that are not only categories and language links");
                    }

                    line = reader.ReadLine();
                }
            }

            return line;
        }

        private static string HandleMarkup(string markupName, RuleResult result, StringReader reader, string line)
        {
            if (line.StartsWith($"<{markupName}"))
            {
                while (!line.StartsWith($"</{markupName}"))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        result.AddIssue($"{markupName} has empty lines.");
                    }

                    var nextLine = reader.ReadLine();

                    if (nextLine is null)
                    {
                        throw new InvalidOperationException("Article content has ended abruptly during markup reading.");
                    }

                    line = nextLine;
                }
            }

            return line;
        }

        private static void HandleNewlines(RuleResult result, string line, string previousLine)
        {
            var wasPreviousLineEmpty = string.IsNullOrWhiteSpace(previousLine);
            if (string.IsNullOrEmpty(line.Trim()))
            {
                if (wasPreviousLineEmpty)
                {
                    result.AddIssue("There is a double newline");
                }
            }
            else
            {
                if (!wasPreviousLineEmpty
                    && !(line.StartsWith('*')
                    || line.StartsWith("__")
                    || previousLine.StartsWith('=')
                    || previousLine.EndsWith("}}")
                    || previousLine.EndsWith("|]")
                    || previousLine.Equals("<br />")))
                {
                    result.AddIssue($"A newline is missing before line: '{line}'");
                }
            }
        }

        private static string HandleTemplates(RuleResult result, StringReader reader, string? line)
        {
            return HandleStructure(result, reader, line, "{{", "}}");
        }

        private static string HandleTables(RuleResult result, StringReader reader, string? line)
        {
            return HandleStructure(result, reader, line, "{|", "|}");
        }

        private static string HandleStructure(RuleResult result, StringReader reader, string? line, string opening, string ending)
        {
            var wasPreviousLineEmpty = false;
            if (line!.Contains(opening))
            {
                while (line != null && !line.Contains(ending))
                {
                    wasPreviousLineEmpty = CheckDoubleNewlinesInStructure(result, line, wasPreviousLineEmpty);

                    line = reader.ReadLine();
                }

                if (line == null)
                {
                    throw new InvalidOperationException("Article content has ended abruptly during structure reading.");
                }

                var indexOfTemplateStart = line.IndexOf(opening);
                var indexOfTemplateEnd = line.LastIndexOf(ending) + ending.Length;
                if (indexOfTemplateStart > 0 || indexOfTemplateEnd < line.Trim().Length)
                {
                    result.AddIssue($"There is a paragraph sharing a line with a template ('{line}')");
                }
            }

            return line;
        }

        private static bool CheckDoubleNewlinesInStructure(RuleResult result, string line, bool wasPreviousLineEmpty)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                if (wasPreviousLineEmpty)
                {
                    result.AddIssue("There is a double newline");
                }

                wasPreviousLineEmpty = true;
            }

            return wasPreviousLineEmpty;
        }
    }
}