using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.WeaponComponents
{
    public class Weapon : IItem, IResearchable
    {
        public ItemType Type => ItemType.Weapon;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public ItemSources? Sources { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
