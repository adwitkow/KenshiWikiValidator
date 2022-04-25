namespace KenshiWikiValidator.WikiTemplates.Creators
{
    public class CraftingTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Crafting";

        public string BuildingName { get; set; }

        public (string Name, int Amount) Input1 { get; set; }

        public (string Name, int Amount)? Input2 { get; set; }

        public string Output { get; set; }

        public string ImageSettings { get; set; }

        public bool Collapsed { get; set; }

        public WikiTemplate Generate()
        {
            var unnamedProperties = new SortedSet<string>();
            var properties = new SortedList<string, string?>()
            {
                { "building", this.BuildingName },
                { "input0", this.Input1.Name },
                { "input0amount", this.Input1.Amount.ToString() },
                { "input1", this.Input2?.Name },
                { "input1amount", this.Input2?.Amount.ToString() },
                { "imagesettings", this.ImageSettings },
                { "output", this.Output },
            };

            if (this.Collapsed)
            {
                unnamedProperties.Add("collapsed");
            }

            return new WikiTemplate(TemplateName, unnamedProperties, properties);
        }
    }
}
