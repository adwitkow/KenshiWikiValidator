using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class LocationalDamage : ItemBase
    {
        public LocationalDamage(string stringId, string name)
            : base(stringId, name)
        {
            this.InjuryAnims = Enumerable.Empty<ItemReference<Animation>>();
            this.PainAnim = Enumerable.Empty<ItemReference<Animation>>();
        }

        public override ItemType Type => ItemType.LocationalDamage;

        [Value("collapses")]
        public bool? Collapses { get; set; }

        [Value("death")]
        public bool? Death { get; set; }

        [Value("severance")]
        public bool? Severance { get; set; }

        [Value("affects move speed")]
        public float? AffectsMoveSpeed { get; set; }

        [Value("affects skills")]
        public float? AffectsSkills { get; set; }

        [Value("KO mult")]
        public float? KoMult { get; set; }

        [Value("body part type")]
        public int? BodyPartType { get; set; }

        [Value("collapse part")]
        public int? CollapsePart { get; set; }

        [Value("bone name")]
        public string? BoneName { get; set; }

        [Value("bone name 2")]
        public string? BoneName2 { get; set; }

        [Value("bone name 3")]
        public string? BoneName3 { get; set; }

        [Reference("injury anims")]
        public IEnumerable<ItemReference<Animation>> InjuryAnims { get; set; }

        [Reference("pain anim")]
        public IEnumerable<ItemReference<Animation>> PainAnim { get; set; }

    }
}