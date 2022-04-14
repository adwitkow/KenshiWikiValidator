using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.WikiSections
{
    public class WikiSectionBuilder
    {
        private TemplateBuilder templateBuilder;

        public WikiSectionBuilder()
        {
            this.WikiSection = new WikiSection();
            this.templateBuilder = new TemplateBuilder();
        }

        public WikiSection WikiSection { get; private set; }

        public WikiSectionBuilder WithHeader(string header)
        {
            this.WikiSection.Header = header;
            return this;
        }

        public WikiSectionBuilder WithNewline()
        {
            this.AddComponent(string.Empty);
            return this;
        }

        public WikiSectionBuilder WithParagraph(string paragraph)
        {
            this.AddComponent(paragraph);
            this.AddComponent(string.Empty);
            return this;
        }

        public WikiSectionBuilder WithLine(string line)
        {
            this.AddComponent(line);
            return this;
        }

        public WikiSectionBuilder WithUnorderedList(IEnumerable<string> list)
        {
            var formattedList = list.Select(item => "* " + item);
            this.AddComponents(formattedList);
            return this;
        }

        public WikiSectionBuilder WithTemplate(WikiTemplate template)
        {
            var builtTemplate = this.templateBuilder.Build(template);
            this.AddComponent(builtTemplate);
            return this;
        }

        public WikiSectionBuilder WithSubsection(string title, int level)
        {
            if (level < 1 || level > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            var prefixSuffix = new string('=', level + 2);
            this.AddComponent($"{prefixSuffix} {title} {prefixSuffix}");
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrEmpty(this.WikiSection.Header))
            {
                throw new InvalidOperationException("Cannot build a WikiSection without a header.");
            }

            var allStrings = new string[] { $"== {this.WikiSection.Header} ==" }.Concat(this.WikiSection.Components);

            return string.Join(Environment.NewLine, allStrings);
        }

        private void AddComponent(string component)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            this.WikiSection.Components.Add(component);
        }

        private void AddComponents(IEnumerable<string> components)
        {
            if (components is null || !components.Any())
            {
                throw new ArgumentNullException(nameof(components));
            }

            this.WikiSection.Components.AddRange(components);
        }
    }
}