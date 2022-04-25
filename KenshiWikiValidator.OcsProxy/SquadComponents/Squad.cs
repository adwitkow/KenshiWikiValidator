using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.SquadComponents
{
    public class Squad : IItem
    {
        public ItemType Type => ItemType.SquadTemplate;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public bool IsShop { get; set; }

        public IEnumerable<ItemReference> Locations { get; set; }
    }
}
