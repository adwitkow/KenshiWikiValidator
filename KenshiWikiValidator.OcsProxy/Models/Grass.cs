using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Grass : ItemBase
    {
        public Grass(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Grass;

        [Value("blackout")]
        public bool? Blackout { get; set; }

        [Value("cross quads")]
        public bool? CrossQuads { get; set; }

        [Value("blackout noise scale")]
        public float? BlackoutNoiseScale { get; set; }

        [Value("blackout zero cutoff")]
        public float? BlackoutZeroCutoff { get; set; }

        [Value("brightness boost")]
        public float? BrightnessBoost { get; set; }

        [Value("cap")]
        public float? Cap { get; set; }

        [Value("density")]
        public float? Density { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max height")]
        public float? MaxHeight { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("max width")]
        public float? MaxWidth { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min height")]
        public float? MinHeight { get; set; }

        [Value("min width")]
        public float? MinWidth { get; set; }

        [Value("noise scale")]
        public float? NoiseScale { get; set; }

        [Value("wind factor")]
        public float? WindFactor { get; set; }

        [Value("zero cutoff")]
        public float? ZeroCutoff { get; set; }

        [Value("color map")]
        public object? ColorMap { get; set; }

        [Value("grass sprite")]
        public object? GrassSprite { get; set; }

    }
}