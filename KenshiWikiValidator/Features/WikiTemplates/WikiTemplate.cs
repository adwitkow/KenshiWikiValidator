namespace KenshiWikiValidator.Features.WikiTemplates
{
    public class WikiTemplate
    {
        public WikiTemplate(string name, SortedList<string, string?> parameters)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public string Name { get; set; }

        public SortedList<string, string?> Parameters { get; set; }
    }
}
