namespace KenshiWikiValidator.OcsProxy.SharedComponents
{
    public class ItemReference
    {
        public ItemReference(string stringId, string name)
        {
            this.StringId = stringId;
            this.Name = name;
        }

        public string StringId { get; set; }

        public string Name { get; set; }
    }
}
