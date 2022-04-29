using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Weather : ItemBase
    {
        public Weather(string stringId, string name)
            : base(stringId, name)
        {
            this.Effects = Enumerable.Empty<ItemReference<Effect>>();
        }

        public override ItemType Type => ItemType.Weather;

        [Value("fog enabled")]
        public bool? FogEnabled { get; set; }

        [Value("affect strength")]
        public float? AffectStrength { get; set; }

        [Value("clouds density")]
        public float? CloudsDensity { get; set; }

        [Value("dust")]
        public float? Dust { get; set; }

        [Value("dust inside")]
        public float? DustInside { get; set; }

        [Value("dust slope")]
        public float? DustSlope { get; set; }

        [Value("effect strength max")]
        public float? EffectStrengthMax { get; set; }

        [Value("effect strength min")]
        public float? EffectStrengthMin { get; set; }

        [Value("end time")]
        public float? EndTime { get; set; }

        [Value("fog distance max")]
        public float? FogDistanceMax { get; set; }

        [Value("fog distance min")]
        public float? FogDistanceMin { get; set; }

        [Value("fog wind max")]
        public float? FogWindMax { get; set; }

        [Value("fog wind min")]
        public float? FogWindMin { get; set; }

        [Value("heat haze")]
        public float? HeatHaze { get; set; }

        [Value("rain intensity")]
        public float? RainIntensity { get; set; }

        [Value("start time")]
        public float? StartTime { get; set; }

        [Value("wetness")]
        public float? Wetness { get; set; }

        [Value("wind intensity")]
        public float? WindIntensity { get; set; }

        [Value("wind speed max")]
        public float? WindSpeedMax { get; set; }

        [Value("wind speed min")]
        public float? WindSpeedMin { get; set; }

        [Value("affect type")]
        public int? AffectType { get; set; }

        [Value("fog color")]
        public int? FogColor { get; set; }

        [Value("sky color mult")]
        public int? SkyColorMult { get; set; }

        [Value("wind update limit")]
        public int? WindUpdateLimit { get; set; }

        [Value("wind update time")]
        public int? WindUpdateTime { get; set; }

        [Reference("effects")]
        public IEnumerable<ItemReference<Effect>> Effects { get; set; }

    }
}