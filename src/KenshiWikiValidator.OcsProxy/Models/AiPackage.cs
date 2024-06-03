// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using OpenConstructionSet.Data;

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