using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class EffectFogVolume : ItemBase
    {
        public EffectFogVolume(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.EffectFogVolume;

        [Value("additive colour")]
        public bool? AdditiveColour { get; set; }

        [Value("ground movement")]
        public bool? GroundMovement { get; set; }

        [Value("alpha")]
        public float? Alpha { get; set; }

        [Value("distance")]
        public float? Distance { get; set; }

        [Value("position 2 x")]
        public float? Position2X { get; set; }

        [Value("position 2 y")]
        public float? Position2Y { get; set; }

        [Value("position 2 z")]
        public float? Position2Z { get; set; }

        [Value("position x")]
        public float? PositionX { get; set; }

        [Value("position y")]
        public float? PositionY { get; set; }

        [Value("position z")]
        public float? PositionZ { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("colour")]
        public int? Colour { get; set; }

        [Value("type")]
        public int? EffectType { get; set; }

    }
}