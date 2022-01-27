using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public class Armour : IItem, IResearchable
    {
        public ItemType Type => ItemType.Armour;

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }

        public Coverage? Coverage { get; set; }

        public ItemSources? Sources { get; set; }

        public IEnumerable<Crafting>? CraftedIn { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintLocations { get; set; }
    }
}
