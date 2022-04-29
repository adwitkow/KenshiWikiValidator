using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AnimationEvent : ItemBase
    {
        public AnimationEvent(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.AnimationEvent;

        [Value("ground effect")]
        public bool? GroundEffect { get; set; }

        [Value("other")]
        public int? Other { get; set; }

        [Value("bone")]
        public string? Bone { get; set; }

        [Value("event")]
        public string? Event { get; set; }

        [Value("override state")]
        public string? OverrideState { get; set; }

        [Value("override value")]
        public string? OverrideValue { get; set; }

        [Value("stop")]
        public string? Stop { get; set; }

    }
}