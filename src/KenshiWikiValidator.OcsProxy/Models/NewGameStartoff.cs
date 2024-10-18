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

using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class NewGameStartoff : ItemBase, IDescriptive
    {
        public NewGameStartoff(ModItem item)
            : base(item)
        {
            this.ForcedRaces = Enumerable.Empty<ItemReference<Race>>();
            this.Squads = Enumerable.Empty<ItemReference<Squad>>();
            this.Characters = Enumerable.Empty<ItemReference<Character>>();
            this.Towns = Enumerable.Empty<ItemReference<Town>>();
            this.FactionRelations = Enumerable.Empty<ItemReference<Faction>>();
            this.Researches = Enumerable.Empty<ItemReference<Research>>();
        }

        public override ItemType Type => ItemType.NewGameStartoff;

        [Value("force start pos")]
        public bool? ForceStartPos { get; set; }

        [Value("money")]
        public int? Money { get; set; }

        [Value("start pos X")]
        public int? StartPosX { get; set; }

        [Value("start pos Z")]
        public int? StartPosZ { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("difficulty")]
        public string? Difficulty { get; set; }

        [Value("style")]
        public string? Style { get; set; }

        [Reference("force race")]
        public IEnumerable<ItemReference<Race>> ForcedRaces { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Squad>> Squads { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Character>> Characters { get; set; }

        [Reference("town")]
        public IEnumerable<ItemReference<Town>> Towns { get; set; }

        [Reference("faction relations")]
        public IEnumerable<ItemReference<Faction>> FactionRelations { get; set; }

        [Reference("research")]
        public IEnumerable<ItemReference<Research>> Researches { get; set; }
    }
}