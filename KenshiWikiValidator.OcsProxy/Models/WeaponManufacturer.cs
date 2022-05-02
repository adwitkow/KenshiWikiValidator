using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class WeaponManufacturer : ItemBase
    {
        public WeaponManufacturer(string stringId, string name)
            : base(stringId, name)
        {
            this.WeaponModels = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
            this.WeaponTypes = Enumerable.Empty<ItemReference<Weapon>>();
        }

        public override ItemType Type => ItemType.WeaponManufacturer;

        [Value("blunt damage mod")]
        public float? BluntDamageMod { get; set; }

        [Value("cut damage mod")]
        public float? CutDamageMod { get; set; }

        [Value("price mod")]
        public float? PriceMod { get; set; }

        [Value("weight mod")]
        public float? WeightMod { get; set; }

        [Value("min cut damage")]
        public int? MinCutDamage { get; set; }

        [Value("company description")]
        public string? CompanyDescription { get; set; }

        [Reference("weapon models")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> WeaponModels { get; set; }

        [Reference("weapon types")]
        public IEnumerable<ItemReference<Weapon>> WeaponTypes { get; set; }

    }
}