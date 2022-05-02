using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AiPackage : ItemBase
    {
        public AiPackage(string stringId, string name)
            : base(stringId, name)
        {
            this.ContractEndTalkPassive = Enumerable.Empty<ItemReference<Dialogue>>();
            this.InheritsFrom = Enumerable.Empty<ItemReference<AiPackage>>();
            this.LeaderAiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.SquadAiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.Squad2AiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.SlaveAiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.LeadsTo = Enumerable.Empty<ItemReference<AiPackage>>();
        }

        public override ItemType Type => ItemType.AiPackage;

        [Value("clears existing jobs")]
        public bool? ClearsExistingJobs { get; set; }

        [Value("signal func")]
        public int? SignalFunc { get; set; }

        [Value("unloaded func")]
        public int? UnloadedFunc { get; set; }

        [Value("end time")]
        public int? EndTime { get; set; }

        [Value("start time")]
        public int? StartTime { get; set; }

        [Reference("contract end talk passive")]
        public IEnumerable<ItemReference<Dialogue>> ContractEndTalkPassive { get; set; }

        [Reference("inherits from")]
        public IEnumerable<ItemReference<AiPackage>> InheritsFrom { get; set; }

        [Reference("Leader AI Goals")]
        public IEnumerable<ItemReference<AiTask>> LeaderAiGoals { get; set; }

        [Reference("Squad AI Goals")]
        public IEnumerable<ItemReference<AiTask>> SquadAiGoals { get; set; }

        [Reference("Squad2 AI Goals")]
        public IEnumerable<ItemReference<AiTask>> Squad2AiGoals { get; set; }

        [Reference("Slave AI Goals")]
        public IEnumerable<ItemReference<AiTask>> SlaveAiGoals { get; set; }

        [Reference("Leads to")]
        public IEnumerable<ItemReference<AiPackage>> LeadsTo { get; set; }

    }
}