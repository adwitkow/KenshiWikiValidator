﻿namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class NewLinesRule : IValidationRule
    {
        public RuleResult Execute(string title, string content)
        {
            var result = new RuleResult();
            using var reader = new StringReader(content);
            var line = reader.ReadLine();

            var wasPreviousLineEmpty = false;
            var firstLine = true;
            while (line != null)
            {
                HandleIgnores(result, reader, line);
                HandleTemplates(result, reader, line);

                if (!firstLine)
                {
                    wasPreviousLineEmpty = HandleNewlines(result, line, wasPreviousLineEmpty);
                }

                firstLine = false;
                line = reader.ReadLine();
            }

            return result;
        }

        private static void HandleIgnores(RuleResult result, StringReader reader, string line)
        {
            if (line.StartsWith("<gallery"))
            {
                while (line != null && !line.StartsWith("</gallery"))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        result.AddIssue("Gallery has empty lines.");
                    }
                    line = reader.ReadLine()!;
                }
            }

            if (line!.Contains("[[Category") || line.Contains("[[ru:"))
            {
                while (line != null)
                {
                    if (!(line.StartsWith("[[") && line.EndsWith("]]")))
                    {
                        result.AddIssue("Not enough newlines among categories/language links");
                    }

                    if (string.IsNullOrEmpty(line))
                    {
                        result.AddIssue("Empty lines among categories/language links");
                    }

                    line = reader.ReadLine()!;
                }
            }
        }

        private static bool HandleNewlines(RuleResult result, string line, bool wasPreviousLineEmpty)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                if (wasPreviousLineEmpty)
                {
                    result.AddIssue("There is a double newline");
                }

                wasPreviousLineEmpty = true;
            }
            else
            {
                if (!wasPreviousLineEmpty && !line.StartsWith("*"))
                {
                    result.AddIssue($"A newline is missing before line: '{line}'");
                }

                wasPreviousLineEmpty = false;
            }

            return wasPreviousLineEmpty;
        }

        private static void HandleTemplates(RuleResult result, StringReader reader, string? line)
        {
            var wasPreviousLineEmpty = false;
            if (line!.Contains("{{"))
            {
                while (line != null && !line.Contains("}}"))
                {
                    if (string.IsNullOrEmpty(line.Trim()))
                    {
                        if (wasPreviousLineEmpty)
                        {
                            result.AddIssue("There is a double newline");
                        }

                        wasPreviousLineEmpty = true;
                    }
                    line = reader.ReadLine();
                    continue;
                }

                if (line == null)
                {
                    return;
                }

                var indexOfTemplateEnd = line.LastIndexOf("}}") + "}}".Length;
                if (indexOfTemplateEnd < line.Length)
                {
                    result.AddIssue($"There is a paragraph sharing a line with a template ('{line}')");
                }
            }

            return;
        }
    }
}