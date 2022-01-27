namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class NewLinesRule : IValidationRule
    {
        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();
            using var reader = new StringReader(content);
            var line = reader.ReadLine();

            var firstLine = true;
            var previousLine = string.Empty;
            while (line != null)
            {
                HandleIgnores(result, reader, line);
                HandleTemplates(result, reader, line);
                HandleTables(result, reader, line);

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

        private static bool HandleNewlines(RuleResult result, string line, string previousLine)
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
                if (!wasPreviousLineEmpty && !(line.StartsWith("*") || previousLine.StartsWith("=")))
                {
                    result.AddIssue($"A newline is missing before line: '{line}'");
                }

                wasPreviousLineEmpty = false;
            }

            return wasPreviousLineEmpty;
        }

        private static void HandleTemplates(RuleResult result, StringReader reader, string? line)
        {
            HandleStructure(result, reader, line, "{{", "}}");
        }

        private static void HandleTables(RuleResult result, StringReader reader, string? line)
        {
            HandleStructure(result, reader, line, "{|", "|}");
        }

        private static void HandleStructure(RuleResult result, StringReader reader, string? line, string opening, string ending)
        {
            var wasPreviousLineEmpty = false;
            if (line!.Contains(opening))
            {
                while (line != null && !line.Contains(ending))
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

                var indexOfTemplateEnd = line.LastIndexOf(ending) + ending.Length;
                if (indexOfTemplateEnd < line.Length)
                {
                    result.AddIssue($"There is a paragraph sharing a line with a template ('{line}')");
                }
            }
        }
    }
}