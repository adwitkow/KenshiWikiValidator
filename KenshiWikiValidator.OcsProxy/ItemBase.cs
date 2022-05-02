using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public abstract class ItemBase : IItem
    {
        protected ItemBase(string stringId, string name)
        {
            this.StringId = stringId;
            this.Name = name;
        }

        public abstract ItemType Type { get; }

        public string StringId { get; }

        public string Name { get; }
    }
}
