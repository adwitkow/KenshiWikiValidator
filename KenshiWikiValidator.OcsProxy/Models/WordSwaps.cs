using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class WordSwaps : ItemBase
    {
        public WordSwaps(string stringId, string name)
            : base(stringId, name)
        {
            this.Lines = Enumerable.Empty<ItemReference<DialogueLine>>();
        }

        public override ItemType Type => ItemType.WordSwaps;

        [Value("persistent use")]
        public bool? PersistentUse { get; set; }

        [Value("chance permanent")]
        public float? ChancePermanent { get; set; }

        [Value("chance temporary")]
        public float? ChanceTemporary { get; set; }

        [Value("repetition limit")]
        public int? RepetitionLimit { get; set; }

        [Value("score bonus")]
        public int? ScoreBonus { get; set; }

        [Value("speaker")]
        public int? Speaker { get; set; }

        [Value("target is type")]
        public int? TargetIsType { get; set; }

        [Reference("lines")]
        public IEnumerable<ItemReference<DialogueLine>> Lines { get; set; }

    }
}