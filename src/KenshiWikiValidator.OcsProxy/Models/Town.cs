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
    public class Town : ItemBase
    {
        public Town(string stringId, string name)
            : base(stringId, name)
        {
            this.BarSquads = Enumerable.Empty<ItemReference<Squad>>();
            this.Factions = Enumerable.Empty<ItemReference<Faction>>();
            this.OverrideTown = Enumerable.Empty<ItemReference<Town>>();
            this.Residents = Enumerable.Empty<ItemReference<Squad>>();
            this.TradeCulture = Enumerable.Empty<ItemReference<ItemsCulture>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
            this.RoamingSquads = Enumerable.Empty<ItemReference<Squad>>();
            this.TradePrices = Enumerable.Empty<ItemReference<Item>>();
            this.DefaultResident = Enumerable.Empty<ItemReference<Squad>>();
            this.DebrisBuilding = Enumerable.Empty<ItemReference<Building>>();
            this.Debris = Enumerable.Empty<ItemReference<NestItem>>();
            this.WorldStates = Enumerable.Empty<ItemReference<WorldEventState>>();
            this.LootSpawn = Enumerable.Empty<ItemReference<VendorList>>();
        }

        public override ItemType Type => ItemType.Town;

        [Value("distant mesh")]
        public bool? DistantMesh { get; set; }

        [Value("is public")]
        public bool? IsPublic { get; set; }

        [Value("is secret")]
        public bool? IsSecret { get; set; }

        [Value("residents override")]
        public bool? ResidentsOverride { get; set; }

        [Value("spawn in town centre")]
        public bool? SpawnInTownCentre { get; set; }

        [Value("no-foliage range")]
        public float? NoFoliageRange { get; set; }

        [Value("size radius")]
        public float? SizeRadius { get; set; }

        [Value("town radius mult")]
        public float? TownRadiusMult { get; set; }

        [Value("gear artifacts max value")]
        public int? GearArtifactsMaxValue { get; set; }

        [Value("gear artifacts min value")]
        public int? GearArtifactsMinValue { get; set; }

        [Value("item artifacts max value")]
        public int? ItemArtifactsMaxValue { get; set; }

        [Value("item artifacts min value")]
        public int? ItemArtifactsMinValue { get; set; }

        [Value("nest resident population")]
        public int? NestResidentPopulation { get; set; }

        [Value("num centrepoints")]
        public int? NumCentrepoints { get; set; }

        [Value("type")]
        public int? TownType { get; set; }

        [Value("unexplored name")]
        public string? UnexploredName { get; set; }

        [Reference("bar squads")]
        public IEnumerable<ItemReference<Squad>> BarSquads { get; set; }

        [Reference("faction")]
        public IEnumerable<ItemReference<Faction>> Factions { get; set; }

        [Reference("override town")]
        public IEnumerable<ItemReference<Town>> OverrideTown { get; set; }

        [Reference("residents")]
        public IEnumerable<ItemReference<Squad>> Residents { get; set; }

        [Reference("trade culture")]
        public IEnumerable<ItemReference<ItemsCulture>> TradeCulture { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

        [Reference("roaming squads")]
        public IEnumerable<ItemReference<Squad>> RoamingSquads { get; set; }

        [Reference("trade prices")]
        public IEnumerable<ItemReference<Item>> TradePrices { get; set; }

        [Reference("default resident")]
        public IEnumerable<ItemReference<Squad>> DefaultResident { get; set; }

        [Reference("debris building")]
        public IEnumerable<ItemReference<Building>> DebrisBuilding { get; set; }

        [Reference("debris")]
        public IEnumerable<ItemReference<NestItem>> Debris { get; set; }

        [Reference("world state")]
        public IEnumerable<ItemReference<WorldEventState>> WorldStates { get; set; }

        [Reference("loot spawn")]
        public IEnumerable<ItemReference<VendorList>> LootSpawn { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}