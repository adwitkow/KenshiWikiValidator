using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public class Weapon : IItem, IResearchable
    {
        public ItemType Type => ItemType.Weapon;

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }

        public ItemSources? Sources { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintLocations { get; set; }
    }
}
