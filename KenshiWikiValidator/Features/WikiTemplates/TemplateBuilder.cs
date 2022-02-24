using System.Text;

namespace KenshiWikiValidator.Features.WikiTemplates
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

            Append(builder, template.Name, newlines);

            foreach (var parameter in template.UnnamedParameters)
            {
                Append(builder, $" | {parameter}", newlines);
            }

            foreach (var pair in template.Parameters)
            {
                if (pair.Value is not null)
                {
                    Append(builder, $" | {pair.Key} = {pair.Value}", newlines);
                }
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
