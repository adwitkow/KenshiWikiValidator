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
    public class FarmData : ItemBase
    {
        public FarmData(string stringId, string name)
            : base(stringId, name)
        {
            this.Plants = Enumerable.Empty<ItemReference<FarmPart>>();
        }

        public override ItemType Type => ItemType.FarmData;

        [Value("inside")]
        public bool? Inside { get; set; }

        [Value("arid")]
        public float? Arid { get; set; }

        [Value("aspect")]
        public float? Aspect { get; set; }

        [Value("clear rate")]
        public float? ClearRate { get; set; }

        [Value("consumption rate")]
        public float? ConsumptionRate { get; set; }

        [Value("death threshold")]
        public float? DeathThreshold { get; set; }

        [Value("death time")]
        public float? DeathTime { get; set; }

        [Value("drought death time")]
        public float? DroughtDeathTime { get; set; }

        [Value("drought multiplier")]
        public float? DroughtMultiplier { get; set; }

        [Value("fertility effect")]
        public float? FertilityEffect { get; set; }

        [Value("green")]
        public float? Green { get; set; }

        [Value("growth time")]
        public float? GrowthTime { get; set; }

        [Value("harvest rate")]
        public float? HarvestRate { get; set; }

        [Value("harvest time")]
        public float? HarvestTime { get; set; }

        [Value("jitter")]
        public float? Jitter { get; set; }

        [Value("minimum fertility")]
        public float? MinimumFertility { get; set; }

        [Value("output per plant")]
        public float? OutputPerPlant { get; set; }

        [Value("spacing")]
        public float? Spacing { get; set; }

        [Value("swamp")]
        public float? Swamp { get; set; }

        [Value("amount")]
        public int? Amount { get; set; }

        [Value("death colour")]
        public int? DeathColour { get; set; }

        [Value("layout")]
        public int? Layout { get; set; }

        [Reference("plants")]
        public IEnumerable<ItemReference<FarmPart>> Plants { get; set; }
    }
}