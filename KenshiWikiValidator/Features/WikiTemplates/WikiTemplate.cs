namespace KenshiWikiValidator.Features.WikiTemplates
{
    public class WikiTemplate
    {
        public WikiTemplate(string name, SortedList<string, string> properties)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public string Name { get; set; }

        public SortedList<string, string> Properties { get; set; }
    }
}
