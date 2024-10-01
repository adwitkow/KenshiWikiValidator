namespace TemplateDataGenerator
{
    public class TemplatePage
    {
        public required string Title { get; init; }

        public required string Content { get; init; }

        public TemplatePage? DocumentationPage { get; set; }

        public int Transclusions { get; set; }
    }
}
