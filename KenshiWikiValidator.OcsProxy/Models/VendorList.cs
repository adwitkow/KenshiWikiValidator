using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class VendorList : ItemBase
    {
        public VendorList(string stringId, string name)
            : base(stringId, name)
        {
            this.ArmourBlueprints = Enumerable.Empty<ItemReference<Armour>>();
            this.Clothing = Enumerable.Empty<ItemReference<Armour>>();
            this.Items = Enumerable.Empty<ItemReference<Item>>();
            this.Blueprints = Enumerable.Empty<ItemReference<Research>>();
            this.WeaponManufacturers = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.Weapons = Enumerable.Empty<ItemReference<Weapon>>();
            this.Containers = Enumerable.Empty<ItemReference<Container>>();
            this.Maps = Enumerable.Empty<ItemReference<MapItem>>();
            this.Crossbows = Enumerable.Empty<ItemReference<Crossbow>>();
            this.CrossbowBlueprints = Enumerable.Empty<ItemReference<Crossbow>>();
            this.Robotics = Enumerable.Empty<ItemReference<LimbReplacement>>();
        }

        public override ItemType Type => ItemType.VendorList;

        [Value("money item prob")]
        public float? MoneyItemProb { get; set; }

        [Value("items count")]
        public int? ItemsCount { get; set; }

        [Value("money item max")]
        public int? MoneyItemMax { get; set; }

        [Value("money item min")]
        public int? MoneyItemMin { get; set; }

        [Reference("armour blueprints")]
        public IEnumerable<ItemReference<Armour>> ArmourBlueprints { get; set; }

        [Reference("clothing")]
        public IEnumerable<ItemReference<Armour>> Clothing { get; set; }

        [Reference("items")]
        public IEnumerable<ItemReference<Item>> Items { get; set; }

        [Reference("blueprints")]
        public IEnumerable<ItemReference<Research>> Blueprints { get; set; }

        [Reference("weapon manufacturers")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponManufacturers { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<Weapon>> Weapons { get; set; }

        [Reference("containers")]
        public IEnumerable<ItemReference<Container>> Containers { get; set; }

        [Reference("maps")]
        public IEnumerable<ItemReference<MapItem>> Maps { get; set; }

        [Reference("crossbows")]
        public IEnumerable<ItemReference<Crossbow>> Crossbows { get; set; }

        [Reference("crossbow blueprints")]
        public IEnumerable<ItemReference<Crossbow>> CrossbowBlueprints { get; set; }

        [Reference("robotics")]
        public IEnumerable<ItemReference<LimbReplacement>> Robotics { get; set; }

    }
}