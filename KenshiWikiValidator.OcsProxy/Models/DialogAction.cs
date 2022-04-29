using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class DialogAction : ItemBase
    {
        public DialogAction(string stringId, string name)
            : base(stringId, name)
        {
            this.LeaderAiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.ContractEndTalkPassive = Enumerable.Empty<ItemReference<Dialogue>>();
        }

        public override ItemType Type => ItemType.DialogAction;

        [Value("action name")]
        public int? ActionName { get; set; }

        [Value("tag")]
        public int? Tag { get; set; }

        [Value("compare by")]
        public object? CompareBy { get; set; }

        [Value("condition name")]
        public int? ConditionName { get; set; }

        [Value("who")]
        public int? Who { get; set; }

        [Value("stringvar")]
        public string? Stringvar { get; set; }

        [Value("action value")]
        public int? ActionValue { get; set; }

        [Value("clears existing jobs")]
        public bool? ClearsExistingJobs { get; set; }

        [Value("signal func")]
        public int? SignalFunc { get; set; }

        [Value("unloaded func")]
        public int? UnloadedFunc { get; set; }

        [Reference("Leader AI Goals")]
        public IEnumerable<ItemReference<AiTask>> LeaderAiGoals { get; set; }

        [Reference("contract end talk passive")]
        public IEnumerable<ItemReference<Dialogue>> ContractEndTalkPassive { get; set; }

    }
}