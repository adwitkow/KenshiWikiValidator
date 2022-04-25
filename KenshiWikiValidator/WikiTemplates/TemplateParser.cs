namespace KenshiWikiValidator.WikiTemplates
{
    public class TemplateParser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "I like it the way it is.")]
        public WikiTemplate Parse(string input)
        {
            var trimmed = input.Trim();
            if (!trimmed.StartsWith("{{"))
            {
                throw new ArgumentException("Input must start with a double brace ('{{')", nameof(input));
            }

            if (!trimmed.EndsWith("}}"))
            {
                throw new ArgumentException("Input must end with a double brace ('{{')", nameof(input));
            }

            trimmed = input.Substring(2, trimmed.Length - 4);

            if (string.IsNullOrEmpty(trimmed))
            {
                throw new ArgumentException("Input does not have any content to parse.", nameof(input));
            }

            var templateElements = trimmed.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(element => element.Trim())
                .ToList();
            var name = templateElements.First();

            var properties = new SortedList<string, string?>();
            for (int i = 1; i < templateElements.Count; i++)
            {
                var element = templateElements[i];
                if (element.Contains('='))
                {
                    var splitElements = element.Split('=');
                    var key = splitElements[0].Trim();
                    var value = splitElements[1].Trim();

                    properties.Add(key, value);
                }
                else
                {
                    properties.Add(i.ToString(), element);
                }
            }

            var result = new WikiTemplate(name, properties);

            return result;
        }
    }
}