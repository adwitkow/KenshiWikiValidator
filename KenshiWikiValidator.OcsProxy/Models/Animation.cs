using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Animation : ItemBase
    {
        public Animation(string stringId, string name)
            : base(stringId, name)
        {
            this.Events = Enumerable.Empty<ItemReference<AnimationEvent>>();
            this.Stumbles = Enumerable.Empty<ItemReference<LocationalDamage>>();
        }

        public override ItemType Type => ItemType.Animation;

        [Value("1 handed")]
        public bool? OneHanded { get; set; }

        [Value("being carried")]
        public bool? BeingCarried { get; set; }

        [Value("big stumble")]
        public bool? BigStumble { get; set; }

        [Value("blunt")]
        public bool? Blunt { get; set; }

        [Value("carrying left")]
        public bool? CarryingLeft { get; set; }

        [Value("carrying right")]
        public bool? CarryingRight { get; set; }

        [Value("delete below waist")]
        public bool? DeleteBelowWaist { get; set; }

        [Value("delete body head")]
        public bool? DeleteBodyHead { get; set; }

        [Value("delete L arm")]
        public bool? DeleteLArm { get; set; }

        [Value("delete R arm")]
        public bool? DeleteRArm { get; set; }

        [Value("delete root")]
        public bool? DeleteRoot { get; set; }

        [Value("delete spine0")]
        public bool? DeleteSpine0 { get; set; }

        [Value("delete tail")]
        public bool? DeleteTail { get; set; }

        [Value("delete weapons")]
        public bool? DeleteWeapons { get; set; }

        [Value("disabled")]
        public bool? Disabled { get; set; }

        [Value("disables movement")]
        public bool? DisablesMovement { get; set; }

        [Value("hackers")]
        public bool? Hackers { get; set; }

        [Value("heavy weapons")]
        public bool? HeavyWeapons { get; set; }

        [Value("idle")]
        public bool? Idle { get; set; }

        [Value("is action")]
        public bool? IsAction { get; set; }

        [Value("katanas")]
        public bool? Katanas { get; set; }

        [Value("loop")]
        public bool? Loop { get; set; }

        [Value("normalise")]
        public bool? Normalise { get; set; }

        [Value("override head")]
        public bool? OverrideHead { get; set; }

        [Value("override L arm")]
        public bool? OverrideLArm { get; set; }

        [Value("override R arm")]
        public bool? OverrideRArm { get; set; }

        [Value("polearm")]
        public bool? Polearm { get; set; }

        [Value("prone")]
        public bool? Prone { get; set; }

        [Value("relocates")]
        public bool? Relocates { get; set; }

        [Value("reverse looping")]
        public bool? ReverseLooping { get; set; }

        [Value("sabre")]
        public bool? Sabre { get; set; }

        [Value("synchs")]
        public bool? Synchs { get; set; }

        [Value("unarmed")]
        public bool? Unarmed { get; set; }

        [Value("uses left arm")]
        public bool? UsesLeftArm { get; set; }

        [Value("uses right arm")]
        public bool? UsesRightArm { get; set; }

        [Value("L leg damage ideal")]
        public float? LLegDamageIdeal { get; set; }

        [Value("L leg damage max")]
        public float? LLegDamageMax { get; set; }

        [Value("L leg damage min")]
        public float? LLegDamageMin { get; set; }

        [Value("max speed")]
        public float? MaxSpeed { get; set; }

        [Value("min speed")]
        public float? MinSpeed { get; set; }

        [Value("move speed")]
        public float? MoveSpeed { get; set; }

        [Value("play speed")]
        public float? PlaySpeed { get; set; }

        [Value("R leg damage ideal")]
        public float? RLegDamageIdeal { get; set; }

        [Value("R leg damage max")]
        public float? RLegDamageMax { get; set; }

        [Value("R leg damage min")]
        public float? RLegDamageMin { get; set; }

        [Value("strafe speed max")]
        public float? StrafeSpeedMax { get; set; }

        [Value("synch offset")]
        public float? SynchOffset { get; set; }

        [Value("category")]
        public int? Category { get; set; }

        [Value("chance")]
        public int? Chance { get; set; }

        [Value("has weapon L")]
        public int? HasWeaponL { get; set; }

        [Value("has weapon R")]
        public int? HasWeaponR { get; set; }

        [Value("idle chance")]
        public int? IdleChance { get; set; }

        [Value("idle time max")]
        public int? IdleTimeMax { get; set; }

        [Value("idle time min")]
        public int? IdleTimeMin { get; set; }

        [Value("is combat mode")]
        public int? IsCombatMode { get; set; }

        [Value("stealth mode")]
        public int? StealthMode { get; set; }

        [Value("stumble from")]
        public int? StumbleFrom { get; set; }

        [Value("weather type")]
        public int? WeatherType { get; set; }

        [Value("anim name")]
        public string? AnimName { get; set; }

        [Value("layer")]
        public string? Layer { get; set; }

        [Value("slave anim")]
        public string? SlaveAnim { get; set; }

        [Reference("events")]
        public IEnumerable<ItemReference<AnimationEvent>> Events { get; set; }

        [Reference("stumbles")]
        public IEnumerable<ItemReference<LocationalDamage>> Stumbles { get; set; }

    }
}