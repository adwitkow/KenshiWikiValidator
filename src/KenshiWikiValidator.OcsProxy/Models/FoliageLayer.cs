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
    public class FoliageLayer : ItemBase
    {
        public FoliageLayer(string stringId, string name)
            : base(stringId, name)
        {
            this.Meshes = Enumerable.Empty<ItemReference<FoliageMesh>>();
            this.Grass = Enumerable.Empty<ItemReference<Grass>>();
        }

        public override ItemType Type => ItemType.FoliageLayer;

        [Value("wind")]
        public bool? Wind { get; set; }

        [Value("visibility range")]
        public int? VisibilityRange { get; set; }

        [Value("uses foliage system")]
        public bool? UsesFoliageSystem { get; set; }

        [Value("lod levels")]
        public int? LodLevels { get; set; }

        [Value("lod range")]
        public int? LodRange { get; set; }

        [Value("page size")]
        public int? PageSize { get; set; }

        [Reference("meshes")]
        public IEnumerable<ItemReference<FoliageMesh>> Meshes { get; set; }

        [Reference("grass")]
        public IEnumerable<ItemReference<Grass>> Grass { get; set; }
    }
}