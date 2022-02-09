using System.Text;

namespace KenshiWikiValidator.Features.WikiTemplates
{
    public class TemplateBuilder
    {
        public string Build(WikiTemplate template)
        {
            if (string.IsNullOrEmpty(template.Name))
            {
                throw new ArgumentException("Template needs to have a name");
            }

            var builder = new StringBuilder("{{");
            builder.AppendLine(template.Name);

            foreach (var pair in template.Properties)
            {
                if (pair.Value is not null)
                {
                    builder.AppendLine($"| {pair.Key} = {pair.Value}");
                }
            }

            builder.Append("}}");

            return builder.ToString();
        }
    }
}
