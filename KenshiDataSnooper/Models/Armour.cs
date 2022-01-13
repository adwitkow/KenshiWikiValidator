using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Models
{
    public class Armour : IItem
    {
        public ItemType Type => ItemType.Armour;

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }

        public Coverage? Coverage { get; set; }

        public IEnumerable<Crafting>? CraftedIn { get; set; }

        public IEnumerable<Character>? AlwaysWornBy { get; set; }

        public IEnumerable<Character>? PotentiallyWornBy { get; set; }

        // TODO: Shops, Loot

        public IEnumerable<DataItem>? UnknownReferences { get; set; }
    }
}
