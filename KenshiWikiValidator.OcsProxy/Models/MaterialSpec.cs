using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class MaterialSpec : ItemBase
    {
        public MaterialSpec(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
        }

        public override ItemType Type => ItemType.MaterialSpec;

        [Value("scaffolding tex scale")]
        public float? ScaffoldingTexScale { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("tile X")]
        public float? TileX { get; set; }

        [Value("tile Y")]
        public float? TileY { get; set; }

        [Value("material type")]
        public int? MaterialType { get; set; }

        [Value("metalness map")]
        public object? MetalnessMap { get; set; }

        [Value("metalness map 2")]
        public object? MetalnessMap2 { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("normal map 2")]
        public object? NormalMap2 { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

        [Value("texture map 2")]
        public object? TextureMap2 { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

    }
}