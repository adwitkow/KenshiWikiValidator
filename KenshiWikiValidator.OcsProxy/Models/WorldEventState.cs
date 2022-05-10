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
    public class WorldEventState : ItemBase
    {
        public WorldEventState(string stringId, string name)
            : base(stringId, name)
        {
            this.NpcIs = Enumerable.Empty<ItemReference<Character>>();
            this.NpcIsNot = Enumerable.Empty<ItemReference<Character>>();
            this.PlayerAlly = Enumerable.Empty<ItemReference<Faction>>();
            this.PlayerEnemy = Enumerable.Empty<ItemReference<Faction>>();
        }

        public override ItemType Type => ItemType.WorldEventState;

        [Value("player involvement")]
        public bool? PlayerInvolvement { get; set; }

        [Value("notes")]
        public string? Notes { get; set; }

        [Reference("NPC is")]
        public IEnumerable<ItemReference<Character>> NpcIs { get; set; }

        [Reference("NPC is NOT")]
        public IEnumerable<ItemReference<Character>> NpcIsNot { get; set; }

        [Reference("player ally")]
        public IEnumerable<ItemReference<Faction>> PlayerAlly { get; set; }

        [Reference("player enemy")]
        public IEnumerable<ItemReference<Faction>> PlayerEnemy { get; set; }
    }
}