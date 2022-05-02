using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class MaterialSpecsWeapon : ItemBase, IDescriptive
    {
        public MaterialSpecsWeapon(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.MaterialSpecsWeapon;

        [Value("craft list fixed")]
        public bool? CraftListFixed { get; set; }

        [Value("overall scale")]
        public float? OverallScale { get; set; }

        [Value("scale length")]
        public float? ScaleLength { get; set; }

        [Value("scale thickness")]
        public float? ScaleThickness { get; set; }

        [Value("scale width")]
        public float? ScaleWidth { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("attack mod")]
        public int? AttackMod { get; set; }

        [Value("defence mod")]
        public int? DefenceMod { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("metalness map")]
        public object? MetalnessMap { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

    }
}