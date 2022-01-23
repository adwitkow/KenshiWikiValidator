namespace KenshiWikiValidator.Validators.Rules
{
    public class NewLinesRule : IValidationRule
    {
        public RuleResult Execute(string content)
        {
            var result = new RuleResult();
            var reader = new StringReader(content);
            var line = reader.ReadLine();

            var lastLineWasTemplate = false;
            while (line != null)
            {
                lastLineWasTemplate = HandleTemplates(result, reader, line);

                line = reader.ReadLine();
            }

            return result;
        }

        private static bool HandleTemplates(RuleResult result, StringReader reader, string? line)
        {
            var lastLineWasTemplate = false;
            if (line.Contains("{{"))
            {
                lastLineWasTemplate = true;
                while (line != null && !line.Contains("}}"))
                {
                    line = reader.ReadLine();
                    // check newlines on multi-line template, read more lines until we hit }}
                    continue;
                }

                if (line == null)
                {
                    return lastLineWasTemplate;
                }

                var indexOfTemplateEnd = line.LastIndexOf("}}") + "}}".Length;
                if (indexOfTemplateEnd < line.Length)
                {
                    result.Success = false;
                }
            }

            return lastLineWasTemplate;
        }
    }
}