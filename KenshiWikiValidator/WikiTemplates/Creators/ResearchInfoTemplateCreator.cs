namespace KenshiWikiValidator.WikiTemplates.Creators
{
    public class ResearchInfoTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Research info";

        public string ResearchName { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public int Time { get; set; }

        public int TechLevel { get; set; }

        public IEnumerable<string> Costs { get; set; }

        public IEnumerable<string> Prerequisites { get; set; }

        public IEnumerable<string> NewBuildings { get; set; }

        public IEnumerable<string> NewItems { get; set; }

        public IEnumerable<string> RequiredFor { get; set; }

        public WikiTemplate Generate()
        {
            var prerequisites = string.Join(", ", this.Prerequisites.Select(item => $"[[{item}]]"));
            var newBuildings = string.Join(", ", this.NewBuildings.Select(item => $"[[{item}]]"));
            var newItems = string.Join(", ", this.NewItems.Select(item => $"[[{item}]]"));
            var requiredFor = string.Join(", ", this.RequiredFor.Select(item => $"[[{item}]]"));
            var costs = string.Join(", ", this.Costs);

            var properties = new SortedList<string, string?>()
            {
                { "name", this.ResearchName },
                { "image", this.Icon },
                { "description", this.Description },
                { "time", this.Time.ToString() },
                { "level", this.TechLevel.ToString() },
                { "prerequisites", prerequisites },
                { "new_bldgs", newBuildings },
                { "new_items", newItems },
                { "costs", costs },
                { "required_for", requiredFor },
            };

            return new WikiTemplate(TemplateName, properties);
        }
    }
}
