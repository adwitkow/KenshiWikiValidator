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
    public class Road : ItemBase
    {
        public Road(string stringId, string name)
            : base(stringId, name)
        {
            this.Spawns = Enumerable.Empty<ItemReference<Squad>>();
        }

        public override ItemType Type => ItemType.Road;

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("population amount")]
        public float? PopulationAmount { get; set; }

        [Value("color channel")]
        public int? ColorChannel { get; set; }

        [Value("spawns max")]
        public int? SpawnsMax { get; set; }

        [Value("spawns min")]
        public int? SpawnsMin { get; set; }

        [Value("imagefile")]
        public object? Imagefile { get; set; }

        [Reference("spawns")]
        public IEnumerable<ItemReference<Squad>> Spawns { get; set; }
    }
}