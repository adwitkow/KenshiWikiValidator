using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AnimalAnimation : ItemBase
    {
        public AnimalAnimation(string stringId, string name)
            : base(stringId, name)
        {
            this.Events = Enumerable.Empty<ItemReference<AnimationEvent>>();
        }

        public override ItemType Type => ItemType.AnimalAnimation;

        [Value("being carried")]
        public bool? BeingCarried { get; set; }

        [Value("big stumble")]
        public bool? BigStumble { get; set; }

        [Value("carrying left")]
        public bool? CarryingLeft { get; set; }

        [Value("carrying right")]
        public bool? CarryingRight { get; set; }

        [Value("delete root")]
        public bool? DeleteRoot { get; set; }

        [Value("disables movement")]
        public bool? DisablesMovement { get; set; }

        [Value("idle")]
        public bool? Idle { get; set; }

        [Value("loop")]
        public bool? Loop { get; set; }

        [Value("normalise")]
        public bool? Normalise { get; set; }

        [Value("relocates")]
        public bool? Relocates { get; set; }

        [Value("reverse looping")]
        public bool? ReverseLooping { get; set; }

        [Value("synchs")]
        public bool? Synchs { get; set; }

        [Value("max speed")]
        public float? MaxSpeed { get; set; }

        [Value("min speed")]
        public float? MinSpeed { get; set; }

        [Value("move speed")]
        public float? MoveSpeed { get; set; }

        [Value("play speed")]
        public float? PlaySpeed { get; set; }

        [Value("strafe speed max")]
        public float? StrafeSpeedMax { get; set; }

        [Value("synch offset")]
        public float? SynchOffset { get; set; }

        [Value("chance")]
        public int? Chance { get; set; }

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

        [Value("anim name")]
        public string? AnimName { get; set; }

        [Value("layer")]
        public string? Layer { get; set; }

        [Reference("events")]
        public IEnumerable<ItemReference<AnimationEvent>> Events { get; set; }

    }
}