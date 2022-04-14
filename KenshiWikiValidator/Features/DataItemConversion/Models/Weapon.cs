using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public class Weapon : IDataItem, IResearchable
    {
        public ItemType Type => ItemType.Weapon;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public ItemSources? Sources { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
