using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public abstract class ItemBase : IItem
    {
        public ItemBase(IDictionary<string, object> properties, string stringId, string name)
        {
            this.Properties = new Dictionary<string, object>(properties);
            this.StringId = stringId;
            this.Name = name;
        }

        public abstract ItemType Type { get; }

        public Dictionary<string, object> Properties { get; }

        public string StringId { get; }

        public string Name { get; }
    }
}
