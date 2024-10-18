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
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class WordSwaps : ItemBase
    {
        public WordSwaps(ModItem item)
            : base(item)
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