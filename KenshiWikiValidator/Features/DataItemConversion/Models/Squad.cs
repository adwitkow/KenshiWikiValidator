using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    internal class Squad : IDataItem
    {
        public ItemType Type => ItemType.SquadTemplate;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public bool IsShop { get; set; }

        public IEnumerable<ItemReference> Locations { get; set; }
    }
}
