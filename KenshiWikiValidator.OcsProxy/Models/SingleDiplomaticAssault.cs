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