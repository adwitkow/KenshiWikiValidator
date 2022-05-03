using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Item : ItemBase, IDescriptive, IAllow2DIconOffset, IAllow3DIconOffset
    {
        public Item(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
            this.Ingredients = Enumerable.Empty<ItemReference<Item>>();
            this.PhysicsAttachment = Enumerable.Empty<ItemReference<CharacterPhysicsAttachment>>();
        }

        public override ItemType Type => ItemType.Item;

        [Value("artifact")]
        public bool? Artifact { get; set; }

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("food crop")]
        public bool? FoodCrop { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("item function!")]
        public bool? ItemFunction2 { get; set; }

        [Value("trade item")]
        public bool? TradeItem { get; set; }

        [Value("charges")]
        public float? Charges { get; set; }

        [Value("icon offset H")]
        public float? IconOffsetH { get; set; }

        [Value("icon offset pitch")]
        public float? IconOffsetPitch { get; set; }

        [Value("icon offset roll")]
        public float? IconOffsetRoll { get; set; }

        [Value("icon offset V")]
        public float? IconOffsetV { get; set; }

        [Value("icon offset yaw")]
        public float? IconOffsetYaw { get; set; }

        [Value("icon zoom")]
        public float? IconZoom { get; set; }

        [Value("profitability")]
        public float? Profitability { get; set; }

        [Value("quality")]
        public float? Quality { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("inventory sound")]
        public int? InventorySound { get; set; }

        [Value("item function")]
        public int? ItemFunction { get; set; }

        [Value("persistent")]
        public int? Persistent { get; set; }

        [Value("production time")]
        public int? ProductionTime { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

        [Value("stackable")]
        public int? Stackable { get; set; }

        [Value("value")]
        public int? Value { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("ground mesh")]
        public object? GroundMesh { get; set; }

        [Value("icon")]
        public object? Icon { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }

        [Reference("ingredients")]
        public IEnumerable<ItemReference<Item>> Ingredients { get; set; }

        [Reference("physics attachment")]
        public IEnumerable<ItemReference<CharacterPhysicsAttachment>> PhysicsAttachment { get; set; }

    }
}