using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class ItemPlacementGroup : ItemBase
    {
        public ItemPlacementGroup(string stringId, string name)
            : base(stringId, name)
        {
            this.Items = Enumerable.Empty<ItemReference<Item>>();
            this.WeaponManufacturers = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.Weapons = Enumerable.Empty<ItemReference<Weapon>>();
            this.Clothing = Enumerable.Empty<ItemReference<Armour>>();
        }

        public override ItemType Type => ItemType.ItemPlacementGroup;

        [Value("random yaw")]
        public bool? RandomYaw { get; set; }

        [Value("max respawn time")]
        public int? MaxRespawnTime { get; set; }

        [Value("min respawn time")]
        public int? MinRespawnTime { get; set; }

        [Value("placeholder")]
        public string? Placeholder { get; set; }

        [Reference("items")]
        public IEnumerable<ItemReference<Item>> Items { get; set; }

        [Reference("weapon manufacturers")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponManufacturers { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<Weapon>> Weapons { get; set; }

        [Reference("clothing")]
        public IEnumerable<ItemReference<Armour>> Clothing { get; set; }

    }
}