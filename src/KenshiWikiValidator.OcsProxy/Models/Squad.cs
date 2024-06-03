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
    public class Squad : ItemBase
    {
        public Squad(string stringId, string name)
            : base(stringId, name)
        {
            this.AiPackages = Enumerable.Empty<ItemReference<AiPackage>>();
            this.Animals2 = Enumerable.Empty<ItemReference<AnimalCharacter>>();
            this.Leader = Enumerable.Empty<ItemReference<Character>>();
            this.Personality = Enumerable.Empty<ItemReference<Personality>>();
            this.Vendors = Enumerable.Empty<ItemReference<VendorList>>();
            this.Faction = Enumerable.Empty<ItemReference<Faction>>();
            this.Characters = Enumerable.Empty<ItemReference<Character>>();
            this.Animals = Enumerable.Empty<ItemReference<AnimalCharacter>>();
            this.DialogLeader = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.Building = Enumerable.Empty<ItemReference<Building>>();
            this.Squad2 = Enumerable.Empty<ItemReference<Character>>();
            this.DialogSquad = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.WorldState = Enumerable.Empty<ItemReference<WorldEventState>>();
            this.Housemates = Enumerable.Empty<ItemReference<Squad>>();
            this.BuildingDislike = Enumerable.Empty<ItemReference<Building>>();
            this.ChoosefromList = Enumerable.Empty<ItemReference<Character>>();
            this.Nest = Enumerable.Empty<ItemReference<Town>>();
            this.RaceOverrides = Enumerable.Empty<ItemReference<Race>>();
            this.Slaves = Enumerable.Empty<ItemReference<Squad>>();
            this.SpecialItems = Enumerable.Empty<ItemReference<Item>>();
            this.SpecialMapItems = Enumerable.Empty<ItemReference<MapItem>>();
            this.DialogAnimal = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.Prisoners = Enumerable.Empty<ItemReference<Squad>>();
        }

        public override ItemType Type => ItemType.SquadTemplate;

        [Value("building public")]
        public bool? BuildingPublic { get; set; }

        [Value("building ruined")]
        public bool? BuildingRuined { get; set; }

        [Value("buys illegal")]
        public bool? BuysIllegal { get; set; }

        [Value("buys stolen")]
        public bool? BuysStolen { get; set; }

        [Value("dont multiply")]
        public bool? DontMultiply { get; set; }

        [Value("is trader")]
        public bool? IsTrader { get; set; }

        [Value("malnourished")]
        public bool? Malnourished { get; set; }

        [Value("patrol approaches towns")]
        public bool? PatrolApproachesTowns { get; set; }

        [Value("public beds")]
        public bool? PublicBeds { get; set; }

        [Value("public day")]
        public bool? PublicDay { get; set; }

        [Value("public night")]
        public bool? PublicNight { get; set; }

        [Value("regenerates")]
        public bool? Regenerates { get; set; }

        [Value("roaming military")]
        public bool? RoamingMilitary { get; set; }

        [Value("sell home")]
        public bool? SellHome { get; set; }

        [Value("animal age random")]
        public float? AnimalAgeRandom { get; set; }

        [Value("blood smell mult")]
        public float? BloodSmellMult { get; set; }

        [Value("buy mult")]
        public float? BuyMult { get; set; }

        [Value("sell mult")]
        public float? SellMult { get; set; }

        [Value("vendors fill rate")]
        public float? VendorsFillRate { get; set; }

        [Value("vendors refresh time")]
        public float? VendorsRefreshTime { get; set; }

        [Value("bed usage cost")]
        public int? BedUsageCost { get; set; }

        [Value("building designation")]
        public int? BuildingDesignation { get; set; }

        [Value("crossbow levels")]
        public int? CrossbowLevels { get; set; }

        [Value("force speed")]
        public int? ForceSpeed { get; set; }

        [Value("gear artifacts base value")]
        public int? GearArtifactsBaseValue { get; set; }

        [Value("initial door state")]
        public int? InitialDoorState { get; set; }

        [Value("item artifacts base value")]
        public int? ItemArtifactsBaseValue { get; set; }

        [Value("num random chars")]
        public int? NumRandomChars { get; set; }

        [Value("num random chars max")]
        public int? NumRandomCharsMax { get; set; }

        [Value("robotics levels")]
        public int? RoboticsLevels { get; set; }

        [Value("vendor money")]
        public int? VendorMoney { get; set; }

        [Value("vendors fill total amount")]
        public int? VendorsFillTotalAmount { get; set; }

        [Value("building name")]
        public string? BuildingName { get; set; }

        [Value("layout exterior")]
        public string? LayoutExterior { get; set; }

        [Value("layout interior")]
        public string? LayoutInterior { get; set; }

        [Reference("AI packages")]
        public IEnumerable<ItemReference<AiPackage>> AiPackages { get; set; }

        [Reference("animals2")]
        public IEnumerable<ItemReference<AnimalCharacter>> Animals2 { get; set; }

        [Reference("leader")]
        public IEnumerable<ItemReference<Character>> Leader { get; set; }

        [Reference("personality")]
        public IEnumerable<ItemReference<Personality>> Personality { get; set; }

        [Reference("vendors")]
        public IEnumerable<ItemReference<VendorList>> Vendors { get; set; }

        [Reference("faction")]
        public IEnumerable<ItemReference<Faction>> Faction { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Character>> Characters { get; set; }

        [Reference("animals")]
        public IEnumerable<ItemReference<AnimalCharacter>> Animals { get; set; }

        [Reference("dialog leader")]
        public IEnumerable<ItemReference<DialoguePackage>> DialogLeader { get; set; }

        [Reference("building")]
        public IEnumerable<ItemReference<Building>> Building { get; set; }

        [Reference("squad2")]
        public IEnumerable<ItemReference<Character>> Squad2 { get; set; }

        [Reference("dialog squad")]
        public IEnumerable<ItemReference<DialoguePackage>> DialogSquad { get; set; }

        [Reference("world state")]
        public IEnumerable<ItemReference<WorldEventState>> WorldState { get; set; }

        [Reference("housemates")]
        public IEnumerable<ItemReference<Squad>> Housemates { get; set; }

        [Reference("building dislike")]
        public IEnumerable<ItemReference<Building>> BuildingDislike { get; set; }

        [Reference("choosefrom list")]
        public IEnumerable<ItemReference<Character>> ChoosefromList { get; set; }

        [Reference("nest")]
        public IEnumerable<ItemReference<Town>> Nest { get; set; }

        [Reference("race override")]
        public IEnumerable<ItemReference<Race>> RaceOverrides { get; set; }

        [Reference("slaves")]
        public IEnumerable<ItemReference<Squad>> Slaves { get; set; }

        [Reference("special items")]
        public IEnumerable<ItemReference<Item>> SpecialItems { get; set; }

        [Reference("special map items")]
        public IEnumerable<ItemReference<MapItem>> SpecialMapItems { get; set; }

        [Reference("dialog animal")]
        public IEnumerable<ItemReference<DialoguePackage>> DialogAnimal { get; set; }

        [Reference("prisoners")]
        public IEnumerable<ItemReference<Squad>> Prisoners { get; set; }

        public bool IsShop => this.AiPackages
            .Any(package => package.Item.LeaderAiGoals
                .Any(goal => "Shopkeeper".Equals(goal.Item.Name)));

        public bool ContainsCharacter(Character character)
        {
            return this.Leader.ContainsItem(character)
                || this.Characters.ContainsItem(character)
                || this.Squad2.ContainsItem(character);
        }

        public IEnumerable<Town> GetLocations(IItemRepository repository)
        {
            var residentFactions = repository.GetItems<Faction>()
                .Where(faction => faction.Residents.ContainsItem(this));

            var factionLocations = repository.GetItems<Town>()
                .Where(town => town.Factions
                    .Any(factionRef => residentFactions.Contains(factionRef.Item)));

            return repository.GetItems<Town>()
                .Where(town => town.Residents.ContainsItem(this))
                .Concat(factionLocations)
                .ToList();
        }
    }
}