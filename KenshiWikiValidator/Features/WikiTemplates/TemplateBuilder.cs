using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                builder.AppendLine($"| {pair.Key} = {pair.Value}");
            }

            builder.Append("}}");

            return builder.ToString();
        }
    }
}
