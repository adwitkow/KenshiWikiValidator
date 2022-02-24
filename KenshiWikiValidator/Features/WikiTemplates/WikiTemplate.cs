namespace KenshiWikiValidator.Features.WikiTemplates
{
    public class WikiTemplate
    {
        public WikiTemplate(string name)
            : this(name, new SortedSet<string>(), new SortedList<string, string?>())
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public WikiTemplate(string name, SortedList<string, string?> parameters)
            : this(name, new SortedSet<string>(), parameters)
        {
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public WikiTemplate(string name, SortedSet<string> unnamedParameters)
            : this(name, unnamedParameters, new SortedList<string, string?>())
        {
            this.UnnamedParameters = unnamedParameters ?? throw new ArgumentNullException(nameof(unnamedParameters));
        }

        public WikiTemplate(string name, SortedSet<string> unnamedParameters, SortedList<string, string?> parameters)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            this.UnnamedParameters = unnamedParameters ?? throw new ArgumentNullException(nameof(unnamedParameters));
        }

        public string Name { get; set; }

        public SortedSet<string> UnnamedParameters { get; private set; }

        public SortedList<string, string?> Parameters { get; private set; }
    }
}
