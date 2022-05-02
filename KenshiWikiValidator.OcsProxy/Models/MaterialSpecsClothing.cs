using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class MaterialSpecsClothing : ItemBase
    {
        public MaterialSpecsClothing(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.MaterialSpecsClothing;

        [Value("paint factor 1")]
        public float? PaintFactor1 { get; set; }

        [Value("paint factor 2")]
        public float? PaintFactor2 { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("material type")]
        public int? MaterialType { get; set; }

        [Value("color map")]
        public object? ColorMap { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

    }
}