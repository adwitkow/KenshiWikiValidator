using System.Text;

namespace KenshiWikiValidator.WikiTemplates
{
    public class TemplateBuilder
    {
        public string Build(WikiTemplate template, bool newlines = true)
        {
            if (string.IsNullOrEmpty(template.Name))
            {
                throw new ArgumentException("Template needs to have a name");
            }

            var builder = new StringBuilder("{{");

            var newlineAfterName = true;
            if ((!template.Parameters.Any() && !template.UnnamedParameters.Any()) || template.UnnamedParameters.Count == 1)
            {
                newlineAfterName = false;
            }

            Append(builder, template.Name, newlineAfterName);

            if (!newlineAfterName && template.UnnamedParameters.Any())
            {
                builder.Append(' ');
            }

            foreach (var parameter in template.UnnamedParameters)
            {
                Append(builder, $" | {parameter}", newlines);
            }

            var validParameters = template.Parameters.Where(pair => pair.Value is not null && !string.IsNullOrWhiteSpace(pair.Value));

            var maxLength = 0;
            if (validParameters.Any())
            {
                maxLength = validParameters.Max(pair => pair.Key.Length);
            }

            foreach (var pair in validParameters)
            {
                var paddedKey = pair.Key.PadRight(maxLength);
                Append(builder, $" | {paddedKey} = {pair.Value}", newlines);
            }

            builder.Append("}}");

            return builder.ToString();
        }

        private static void Append(StringBuilder builder, string toAppend, bool newlines)
        {
            if (newlines)
            {
                builder.AppendLine(toAppend.TrimStart());
            }
            else
            {
                builder.Append(toAppend);
            }
        }
    }
}
