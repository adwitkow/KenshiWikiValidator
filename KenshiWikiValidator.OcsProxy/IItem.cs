using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public interface IItem
    {
        public ItemType Type { get; }

        public string StringId { get; }

        public string Name { get; }
    }
}
