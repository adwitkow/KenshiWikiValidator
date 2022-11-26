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

using KenshiWikiValidator.OcsProxy.DialogueComponents;
using OpenConstructionSet.Data;

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
        public DialogueEffect ActionName { get; set; }

        [Value("tag")]
        public int? Tag { get; set; }

        [Value("compare by")]
        public object? CompareBy { get; set; }

        [Value("condition name")]
        public DialogueCondition ConditionName { get; set; }

        [Value("who")]
        public DialogueSpeaker Who { get; set; }

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