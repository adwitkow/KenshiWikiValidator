using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class WildlifeBirds : ItemBase
    {
        public WildlifeBirds(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.WildlifeBirds;

        [Value("circling")]
        public bool? Circling { get; set; }

        [Value("flocking")]
        public bool? Flocking { get; set; }

        [Value("contour strength")]
        public float? ContourStrength { get; set; }

        [Value("distance")]
        public float? Distance { get; set; }

        [Value("ground height")]
        public float? GroundHeight { get; set; }

        [Value("ground strength")]
        public float? GroundStrength { get; set; }

        [Value("height")]
        public float? Height { get; set; }

        [Value("scale max")]
        public float? ScaleMax { get; set; }

        [Value("scale min")]
        public float? ScaleMin { get; set; }

        [Value("spawn height max")]
        public float? SpawnHeightMax { get; set; }

        [Value("spawn height min")]
        public float? SpawnHeightMin { get; set; }

        [Value("speed")]
        public float? Speed { get; set; }

        [Value("spawn count")]
        public int? SpawnCount { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("texture")]
        public object? Texture { get; set; }

    }
}