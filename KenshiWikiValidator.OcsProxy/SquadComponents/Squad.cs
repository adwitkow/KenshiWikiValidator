using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.SquadComponents
{
    public class Squad : ItemBase
    {
        public Squad(Dictionary<string, object> properties, string stringId, string name) : base(properties, stringId, name)
        {
            this.Locations = Enumerable.Empty<ItemReference>();
        }

        public override ItemType Type => ItemType.SquadTemplate;

        public bool IsShop { get; set; }

        public IEnumerable<ItemReference> Locations { get; set; }
    }
}
