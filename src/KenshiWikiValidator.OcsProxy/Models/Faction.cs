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
    public class Faction : ItemBase
    {
        public Faction(ModItem item)
            : base(item)
        {
            this.BarSquads = Enumerable.Empty<ItemReference<Squad>>();
            this.Biomes = Enumerable.Empty<ItemReference<BiomeGroup>>();
            this.BuildingsReplacements = Enumerable.Empty<ItemReference<BuildingsSwap>>();
            this.Campaigns = Enumerable.Empty<ItemReference<FactionCampaign>>();
            this.Color = Enumerable.Empty<ItemReference<ColorData>>();
            this.DefaultResident = Enumerable.Empty<ItemReference<Squad>>();
            this.DialogDefault = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.Hairstyles = Enumerable.Empty<ItemReference<Attachment>>();
            this.LegalSystem = Enumerable.Empty<ItemReference<Faction>>();
            this.NoGoZones = Enumerable.Empty<ItemReference<BiomeGroup>>();
            this.Races = Enumerable.Empty<ItemReference<Race>>();
            this.Relations = Enumerable.Empty<ItemReference<Faction>>();
            this.Residents = Enumerable.Empty<ItemReference<Squad>>();
            this.SlaveClothing = Enumerable.Empty<ItemReference<Armour>>();
            this.SquadDefault = Enumerable.Empty<ItemReference<Squad>>();
            this.Squads = Enumerable.Empty<ItemReference<Squad>>();
            this.TradeCulture = Enumerable.Empty<ItemReference<ItemsCulture>>();
            this.Coexistence = Enumerable.Empty<ItemReference<Faction>>();
            this.Personality = Enumerable.Empty<ItemReference<Personality>>();
            this.ForbiddenItems = Enumerable.Empty<ItemReference<Item>>();
            this.ItemSpawnsResident = Enumerable.Empty<ItemReference<VendorList>>();
            this.ItemSpawnsHq = Enumerable.Empty<ItemReference<VendorList>>();
            this.ItemSpawnsArmoury = Enumerable.Empty<ItemReference<VendorList>>();
            this.ItemSpawnsResidentSmall = Enumerable.Empty<ItemReference<VendorList>>();
            this.Assaults = Enumerable.Empty<ItemReference<DiplomaticAssaults>>();
            this.SpecialSquads = Enumerable.Empty<ItemReference<UniqueSquadTemplate>>();
            this.AiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.ItemSpawnsBar = Enumerable.Empty<ItemReference<VendorList>>();
            this.RoamingSquads = Enumerable.Empty<ItemReference<Squad>>();
            this.AiFallback = Enumerable.Empty<ItemReference<AiPackage>>();
        }

        public override ItemType Type => ItemType.Faction;

        [Value("allow slaves weapons")]
        public bool? AllowSlavesWeapons { get; set; }

        [Value("anti slavery")]
        public bool? AntiSlavery { get; set; }

        [Value("heals strangers")]
        public bool? HealsStrangers { get; set; }

        [Value("not real")]
        public bool? NotReal { get; set; }

        [Value("offers bounties")]
        public bool? OffersBounties { get; set; }

        [Value("building cost mult")]
        public float? BuildingCostMult { get; set; }

        [Value("effect of anger")]
        public float? EffectOfAnger { get; set; }

        [Value("effect of happy")]
        public float? EffectOfHappy { get; set; }

        [Value("emotion fade rate")]
        public float? EmotionFadeRate { get; set; }

        [Value("faces weirdness")]
        public float? FacesWeirdness { get; set; }

        [Value("road preference")]
        public float? RoadPreference { get; set; }

        [Value("run away ratio of squad size")]
        public float? RunAwayRatioOfSquadSize { get; set; }

        [Value("run away ratio relative to enemy")]
        public float? RunAwayRatioRelativeToEnemy { get; set; }

        [Value("trustworthy")]
        public float? Trustworthy { get; set; }

        [Value("armors 0")]
        public int? Armors0 { get; set; }

        [Value("armors 1")]
        public int? Armors1 { get; set; }

        [Value("armors 2")]
        public int? Armors2 { get; set; }

        [Value("armors 3")]
        public int? Armors3 { get; set; }

        [Value("armors 4")]
        public int? Armors4 { get; set; }

        [Value("armors 5")]
        public int? Armors5 { get; set; }

        [Value("business relations")]
        public int? BusinessRelations { get; set; }

        [Value("cages lock level")]
        public int? CagesLockLevel { get; set; }

        [Value("containers lock level")]
        public int? ContainersLockLevel { get; set; }

        [Value("default relation")]
        public int? DefaultRelation { get; set; }

        [Value("doors lock level")]
        public int? DoorsLockLevel { get; set; }

        [Value("enemy classification")]
        public int? EnemyClassification { get; set; }

        [Value("fundamental type")]
        public int? FundamentalType { get; set; }

        [Value("lock level random")]
        public int? LockLevelRandom { get; set; }

        [Value("max prosperity")]
        public int? MaxProsperity { get; set; }

        [Value("num negative ranks")]
        public int? NumNegativeRanks { get; set; }

        [Value("num ranks")]
        public int? NumRanks { get; set; }

        [Value("roaming population")]
        public int? RoamingPopulation { get; set; }

        [Value("squad formation")]
        public int? SquadFormation { get; set; }

        [Value("negative rank0")]
        public string? NegativeRank0 { get; set; }

        [Value("negative rank1")]
        public string? NegativeRank1 { get; set; }

        [Value("negative rank2")]
        public string? NegativeRank2 { get; set; }

        [Value("negative rank3")]
        public string? NegativeRank3 { get; set; }

        [Value("negative rank4")]
        public string? NegativeRank4 { get; set; }

        [Value("rank0")]
        public string? Rank0 { get; set; }

        [Value("rank1")]
        public string? Rank1 { get; set; }

        [Value("rank2")]
        public string? Rank2 { get; set; }

        [Value("rank3")]
        public string? Rank3 { get; set; }

        [Value("rank4")]
        public string? Rank4 { get; set; }

        [Value("rank5")]
        public string? Rank5 { get; set; }

        [Value("rank")]
        public string? Rank { get; set; }

        [Value("negative rank5")]
        public string? NegativeRank5 { get; set; }

        [Value("rank6")]
        public string? Rank6 { get; set; }

        [Value("rank7")]
        public string? Rank7 { get; set; }

        [Reference("bar squads")]
        public IEnumerable<ItemReference<Squad>> BarSquads { get; set; }

        [Reference("biomes")]
        public IEnumerable<ItemReference<BiomeGroup>> Biomes { get; set; }

        [Reference("buildings replacements")]
        public IEnumerable<ItemReference<BuildingsSwap>> BuildingsReplacements { get; set; }

        [Reference("campaigns")]
        public IEnumerable<ItemReference<FactionCampaign>> Campaigns { get; set; }

        [Reference("color")]
        public IEnumerable<ItemReference<ColorData>> Color { get; set; }

        [Reference("default resident")]
        public IEnumerable<ItemReference<Squad>> DefaultResident { get; set; }

        [Reference("dialog default")]
        public IEnumerable<ItemReference<DialoguePackage>> DialogDefault { get; set; }

        [Reference("hairstyles")]
        public IEnumerable<ItemReference<Attachment>> Hairstyles { get; set; }

        [Reference("legal system")]
        public IEnumerable<ItemReference<Faction>> LegalSystem { get; set; }

        [Reference("no-go zones")]
        public IEnumerable<ItemReference<BiomeGroup>> NoGoZones { get; set; }

        [Reference("races")]
        public IEnumerable<ItemReference<Race>> Races { get; set; }

        [Reference("relations")]
        public IEnumerable<ItemReference<Faction>> Relations { get; set; }

        [Reference("residents")]
        public IEnumerable<ItemReference<Squad>> Residents { get; set; }

        [Reference("slave clothing")]
        public IEnumerable<ItemReference<Armour>> SlaveClothing { get; set; }

        [Reference("squad default")]
        public IEnumerable<ItemReference<Squad>> SquadDefault { get; set; }

        [Reference("squads")]
        public IEnumerable<ItemReference<Squad>> Squads { get; set; }

        [Reference("trade culture")]
        public IEnumerable<ItemReference<ItemsCulture>> TradeCulture { get; set; }

        [Reference("coexistence")]
        public IEnumerable<ItemReference<Faction>> Coexistence { get; set; }

        [Reference("personality")]
        public IEnumerable<ItemReference<Personality>> Personality { get; set; }

        [Reference("forbidden items")]
        public IEnumerable<ItemReference<Item>> ForbiddenItems { get; set; }

        [Reference("item spawns resident")]
        public IEnumerable<ItemReference<VendorList>> ItemSpawnsResident { get; set; }

        [Reference("item spawns HQ")]
        public IEnumerable<ItemReference<VendorList>> ItemSpawnsHq { get; set; }

        [Reference("item spawns armoury")]
        public IEnumerable<ItemReference<VendorList>> ItemSpawnsArmoury { get; set; }

        [Reference("item spawns resident small")]
        public IEnumerable<ItemReference<VendorList>> ItemSpawnsResidentSmall { get; set; }

        [Reference("assaults")]
        public IEnumerable<ItemReference<DiplomaticAssaults>> Assaults { get; set; }

        [Reference("special squads")]
        public IEnumerable<ItemReference<UniqueSquadTemplate>> SpecialSquads { get; set; }

        [Reference("AI Goals")]
        public IEnumerable<ItemReference<AiTask>> AiGoals { get; set; }

        [Reference("item spawns bar")]
        public IEnumerable<ItemReference<VendorList>> ItemSpawnsBar { get; set; }

        [Reference("roaming squads")]
        public IEnumerable<ItemReference<Squad>> RoamingSquads { get; set; }

        [Reference("AI fallback")]
        public IEnumerable<ItemReference<AiPackage>> AiFallback { get; set; }
    }
}