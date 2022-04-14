using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public class Armour : IDataItem, IResearchable
    {
        public ItemType Type => ItemType.Armour;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public Coverage? Coverage { get; set; }

        public ItemSources? Sources { get; set; }

        public IEnumerable<Crafting>? CraftedIn { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
