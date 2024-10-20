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
    public class FactionCampaign : ItemBase
    {
        public FactionCampaign(ModItem item)
            : base(item)
        {
            this.Pt1AiLeader = Enumerable.Empty<ItemReference<AiPackage>>();
            this.Pt1AiOthers = Enumerable.Empty<ItemReference<AiPackage>>();
            this.Pt2AiLeader = Enumerable.Empty<ItemReference<AiPackage>>();
            this.RetreatAiLeader = Enumerable.Empty<ItemReference<AiPackage>>();
            this.VictoryAiLeader = Enumerable.Empty<ItemReference<AiPackage>>();
            this.DialogAnnouncement = Enumerable.Empty<ItemReference<Dialogue>>();
            this.InheritsFrom = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.LossTrigger = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.WorldState = Enumerable.Empty<ItemReference<WorldEventState>>();
            this.FactionOverride = Enumerable.Empty<ItemReference<Faction>>();
            this.SquadsToUse = Enumerable.Empty<ItemReference<Squad>>();
            this.VictoryTrigger = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.TriggerPlayerAlly = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.SpecialLeader = Enumerable.Empty<ItemReference<Character>>();
            this.Pt2AiOthers = Enumerable.Empty<ItemReference<AiPackage>>();
            this.RetreatAiOthers = Enumerable.Empty<ItemReference<AiPackage>>();
        }

        public override ItemType Type => ItemType.FactionCampaign;

        [Value("can talk before arrival")]
        public bool? CanTalkBeforeArrival { get; set; }

        [Value("ignores chance mults")]
        public bool? IgnoresChanceMults { get; set; }

        [Value("ignores nogo zones")]
        public bool? IgnoresNogoZones { get; set; }

        [Value("is hostile")]
        public bool? IsHostile { get; set; }

        [Value("target characters")]
        public bool? TargetCharacters { get; set; }

        [Value("territorial triggers")]
        public bool? TerritorialTriggers { get; set; }

        [Value("chance per day")]
        public float? ChancePerDay { get; set; }

        [Value("range far")]
        public float? RangeFar { get; set; }

        [Value("range near")]
        public float? RangeNear { get; set; }

        [Value("repeat limit")]
        public float? RepeatLimit { get; set; }

        [Value("key")]
        public int? Key { get; set; }

        [Value("num forces")]
        public int? NumForces { get; set; }

        [Value("repetition limit")]
        public int? RepetitionLimit { get; set; }

        [Value("target population min")]
        public int? TargetPopulationMin { get; set; }

        [Value("target tech level >=")]
        public int? PlayerTownSizeLevelEqualOrHigherThan { get; set; }

        [Value("travel speed loaded")]
        public int? TravelSpeedLoaded { get; set; }

        [Value("travel speed unloaded")]
        public int? TravelSpeedUnloaded { get; set; }

        [Value("display name")]
        public string? DisplayName { get; set; }

        [Reference("pt1 AI leader")]
        public IEnumerable<ItemReference<AiPackage>> Pt1AiLeader { get; set; }

        [Reference("pt1 AI others")]
        public IEnumerable<ItemReference<AiPackage>> Pt1AiOthers { get; set; }

        [Reference("pt2 AI leader")]
        public IEnumerable<ItemReference<AiPackage>> Pt2AiLeader { get; set; }

        [Reference("retreat AI leader")]
        public IEnumerable<ItemReference<AiPackage>> RetreatAiLeader { get; set; }

        [Reference("victory AI leader")]
        public IEnumerable<ItemReference<AiPackage>> VictoryAiLeader { get; set; }

        [Reference("dialog announcement")]
        public IEnumerable<ItemReference<Dialogue>> DialogAnnouncement { get; set; }

        [Reference("inherits from")]
        public IEnumerable<ItemReference<FactionCampaign>> InheritsFrom { get; set; }

        [Reference("loss trigger")]
        public IEnumerable<ItemReference<FactionCampaign>> LossTrigger { get; set; }

        [Reference("world state")]
        public IEnumerable<ItemReference<WorldEventState>> WorldState { get; set; }

        [Reference("faction override")]
        public IEnumerable<ItemReference<Faction>> FactionOverride { get; set; }

        [Reference("squads to use")]
        public IEnumerable<ItemReference<Squad>> SquadsToUse { get; set; }

        [Reference("victory trigger")]
        public IEnumerable<ItemReference<FactionCampaign>> VictoryTrigger { get; set; }

        [Reference("trigger player ally")]
        public IEnumerable<ItemReference<FactionCampaign>> TriggerPlayerAlly { get; set; }

        [Reference("special leader")]
        public IEnumerable<ItemReference<Character>> SpecialLeader { get; set; }

        [Reference("pt2 AI others")]
        public IEnumerable<ItemReference<AiPackage>> Pt2AiOthers { get; set; }

        [Reference("retreat AI others")]
        public IEnumerable<ItemReference<AiPackage>> RetreatAiOthers { get; set; }
    }
}