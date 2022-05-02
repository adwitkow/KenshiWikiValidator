using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class SingleDiplomaticAssault : ItemBase
    {
        public SingleDiplomaticAssault(string stringId, string name)
            : base(stringId, name)
        {
            this.Conditions = Enumerable.Empty<ItemReference<DialogAction>>();
            this.Dialogue = Enumerable.Empty<ItemReference<Dialogue>>();
            this.DialogueAnnounce = Enumerable.Empty<ItemReference<Dialogue>>();
            this.MainSquad = Enumerable.Empty<ItemReference<Squad>>();
            this.DeliveryAiPackage = Enumerable.Empty<ItemReference<AiPackage>>();
            this.DialogueSquad = Enumerable.Empty<ItemReference<Dialogue>>();
            this.FallbackAiPackage = Enumerable.Empty<ItemReference<AiPackage>>();
            this.AiPackages = Enumerable.Empty<ItemReference<AiPackage>>();
        }

        public override ItemType Type => ItemType.SingleDiplomaticAssault;

        [Value("is aggressive")]
        public bool? IsAggressive { get; set; }

        [Value("repeat timer hours max")]
        public int? RepeatTimerHoursMax { get; set; }

        [Value("repeat timer hours min")]
        public int? RepeatTimerHoursMin { get; set; }

        [Reference("conditions")]
        public IEnumerable<ItemReference<DialogAction>> Conditions { get; set; }

        [Reference("dialogue")]
        public IEnumerable<ItemReference<Dialogue>> Dialogue { get; set; }

        [Reference("dialogue announce")]
        public IEnumerable<ItemReference<Dialogue>> DialogueAnnounce { get; set; }

        [Reference("main squad")]
        public IEnumerable<ItemReference<Squad>> MainSquad { get; set; }

        [Reference("delivery AI package")]
        public IEnumerable<ItemReference<AiPackage>> DeliveryAiPackage { get; set; }

        [Reference("dialogue squad")]
        public IEnumerable<ItemReference<Dialogue>> DialogueSquad { get; set; }

        [Reference("fallback AI package")]
        public IEnumerable<ItemReference<AiPackage>> FallbackAiPackage { get; set; }

        [Reference("AI packages")]
        public IEnumerable<ItemReference<AiPackage>> AiPackages { get; set; }

    }
}