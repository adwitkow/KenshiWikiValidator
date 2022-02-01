using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    internal class Squad : IItem
    {
        public ItemType Type => ItemType.VendorList;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public bool IsShop { get; set; }

        public IEnumerable<ItemReference> Locations { get; set; }
    }
}
