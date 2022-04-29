using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AmbientSound : ItemBase
    {
        public AmbientSound(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.AmbientSound;

        [Value("needs power")]
        public bool? NeedsPower { get; set; }

        [Value("chance")]
        public float? Chance { get; set; }

        [Value("cutoff")]
        public float? Cutoff { get; set; }

        [Value("efficiency multiplier")]
        public float? EfficiencyMultiplier { get; set; }

        [Value("intensity")]
        public float? Intensity { get; set; }

        [Value("emitter")]
        public string? Emitter { get; set; }

    }
}