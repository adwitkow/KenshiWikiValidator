using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Dialogue : ItemBase
    {
        public Dialogue(string stringId, string name)
            : base(stringId, name)
        {
            this.Conditions = Enumerable.Empty<ItemReference<DialogAction>>();
            this.Lines = Enumerable.Empty<ItemReference<DialogueLine>>();
            this.InTownOf = Enumerable.Empty<ItemReference<Faction>>();
            this.WorldState = Enumerable.Empty<ItemReference<WorldEventState>>();
            this.TargetHasItem = Enumerable.Empty<ItemReference<Item>>();
            this.IsCharacter = Enumerable.Empty<ItemReference<Character>>();
            this.TargetFaction = Enumerable.Empty<ItemReference<Faction>>();
            this.TargetRace = Enumerable.Empty<ItemReference<Race>>();
            this.MyRace = Enumerable.Empty<ItemReference<Race>>();
        }

        public override ItemType Type => ItemType.Dialogue;

        [Value("for enemies")]
        public bool? ForEnemies { get; set; }

        [Value("locked")]
        public bool? Locked { get; set; }

        [Value("monologue")]
        public bool? Monologue { get; set; }

        [Value("one at a time")]
        public bool? OneAtATime { get; set; }

        [Value("chance permanent")]
        public float? ChancePermanent { get; set; }

        [Value("chance temporary")]
        public float? ChanceTemporary { get; set; }

        [Value("repetition limit")]
        public int? RepetitionLimit { get; set; }

        [Value("score bonus")]
        public int? ScoreBonus { get; set; }

        [Value("target is type")]
        public int? TargetIsType { get; set; }

        [Value("once only")]
        public bool? OnceOnly { get; set; }

        [Value("speaker")]
        public int? Speaker { get; set; }

        [Reference("conditions")]
        public IEnumerable<ItemReference<DialogAction>> Conditions { get; set; }

        [Reference("lines")]
        public IEnumerable<ItemReference<DialogueLine>> Lines { get; set; }

        [Reference("in town of")]
        public IEnumerable<ItemReference<Faction>> InTownOf { get; set; }

        [Reference("world state")]
        public IEnumerable<ItemReference<WorldEventState>> WorldState { get; set; }

        [Reference("target has item")]
        public IEnumerable<ItemReference<Item>> TargetHasItem { get; set; }

        [Reference("is character")]
        public IEnumerable<ItemReference<Character>> IsCharacter { get; set; }

        [Reference("target faction")]
        public IEnumerable<ItemReference<Faction>> TargetFaction { get; set; }

        [Reference("target race")]
        public IEnumerable<ItemReference<Race>> TargetRace { get; set; }

        [Reference("my race")]
        public IEnumerable<ItemReference<Race>> MyRace { get; set; }

    }
}