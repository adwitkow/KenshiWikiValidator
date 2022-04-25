namespace KenshiWikiValidator.WikiSections
{
    public class WikiSection
    {
        public WikiSection()
        {
            this.Header = string.Empty;
            this.Components = new List<string>();
        }

        public string Header { get; set; }

        public List<string> Components { get; }
    }
}
