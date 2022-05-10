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
    public class Character : ItemBase
    {
        public Character(string stringId, string name)
            : base(stringId, name)
        {
            this.Clothing = Enumerable.Empty<ItemReference<Armour>>();
            this.DialoguePackage = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.DialoguePackagePlayer = Enumerable.Empty<ItemReference<DialoguePackage>>();
            this.Stats = Enumerable.Empty<ItemReference<Stats>>();
            this.Weapons = Enumerable.Empty<ItemReference<Weapon>>();
            this.Backpack = Enumerable.Empty<ItemReference<Container>>();
            this.Dialogue = Enumerable.Empty<ItemReference<Dialogue>>();
            this.Inventory = Enumerable.Empty<ItemReference<Item>>();
            this.AiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.Personality = Enumerable.Empty<ItemReference<Personality>>();
            this.Race = Enumerable.Empty<ItemReference<Race>>();
            this.Faction = Enumerable.Empty<ItemReference<Faction>>();
            this.Shopping = Enumerable.Empty<ItemReference<Item>>();
            this.WeaponLevel = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.BountyFactions = Enumerable.Empty<ItemReference<Faction>>();
            this.Color = Enumerable.Empty<ItemReference<ColorData>>();
            this.AnnouncementDialogue = Enumerable.Empty<ItemReference<Dialogue>>();
            this.Crossbows = Enumerable.Empty<ItemReference<Crossbow>>();
            this.UniqueReplacementSpawn = Enumerable.Empty<ItemReference<Character>>();
            this.DeathItems = Enumerable.Empty<ItemReference<Item>>();
            this.StartingHealth = Enumerable.Empty<ItemReference<LocationalDamage>>();
            this.Blueprints = Enumerable.Empty<ItemReference<Research>>();
            this.Vendors = Enumerable.Empty<ItemReference<VendorList>>();
        }

        public override ItemType Type => ItemType.Character;

        [Value("assigns bounties")]
        public bool? AssignsBounties { get; set; }

        [Value("dumb")]
        public bool? Dumb { get; set; }

        [Value("is trader")]
        public bool? IsTrader { get; set; }

        [Value("named")]
        public bool? Named { get; set; }

        [Value("shaved")]
        public bool? Shaved { get; set; }

        [Value("unique")]
        public bool? Unique { get; set; }

        [Value("wears uniform")]
        public bool? WearsUniform { get; set; }

        [Value("faction importance")]
        public float? FactionImportance { get; set; }

        [Value("money item prob")]
        public float? MoneyItemProb { get; set; }

        [Value("armour grade")]
        public int? ArmourGrade { get; set; }

        [Value("armour upgrade chance")]
        public int? ArmourUpgradeChance { get; set; }

        [Value("bounty amount")]
        public int? BountyAmount { get; set; }

        [Value("bounty amount fuzz")]
        public int? BountyAmountFuzz { get; set; }

        [Value("bounty chance")]
        public int? BountyChance { get; set; }

        [Value("combat stats")]
        public int? CombatStats { get; set; }

        [Value("female chance")]
        public int? FemaleChance { get; set; }

        [Value("money item max")]
        public int? MoneyItemMax { get; set; }

        [Value("money item min")]
        public int? MoneyItemMin { get; set; }

        [Value("money max")]
        public int? MoneyMax { get; set; }

        [Value("money min")]
        public int? MoneyMin { get; set; }

        [Value("NPC class")]
        public int? NpcClass { get; set; }

        [Value("ranged stats")]
        public int? RangedStats { get; set; }

        [Value("slave")]
        public int? Slave { get; set; }

        [Value("stats randomise")]
        public int? StatsRandomise { get; set; }

        [Value("stealth stats")]
        public int? StealthStats { get; set; }

        [Value("strength")]
        public int? Strength { get; set; }

        [Value("unarmed stats")]
        public int? UnarmedStats { get; set; }

        [Value("wages")]
        public int? Wages { get; set; }

        [Value("body")]
        public object? Body { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("diplomatic status")]
        public bool? DiplomaticStatus { get; set; }

        [Value("is law enforcer")]
        public bool? IsLawEnforcer { get; set; }

        [Value("is military")]
        public bool? IsMilitary { get; set; }

        [Value("is peaceful")]
        public bool? IsPeaceful { get; set; }

        [Value("armour level")]
        public int? ArmourLevel { get; set; }

        [Value("max inventory level")]
        public int? MaxInventoryLevel { get; set; }

        [Value("min inventory level")]
        public int? MinInventoryLevel { get; set; }

        [Reference("clothing")]
        public IEnumerable<ItemReference<Armour>> Clothing { get; set; }

        [Reference("dialogue package")]
        public IEnumerable<ItemReference<DialoguePackage>> DialoguePackage { get; set; }

        [Reference("dialogue package player")]
        public IEnumerable<ItemReference<DialoguePackage>> DialoguePackagePlayer { get; set; }

        [Reference("stats")]
        public IEnumerable<ItemReference<Stats>> Stats { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<Weapon>> Weapons { get; set; }

        [Reference("backpack")]
        public IEnumerable<ItemReference<Container>> Backpack { get; set; }

        [Reference("dialogue")]
        public IEnumerable<ItemReference<Dialogue>> Dialogue { get; set; }

        [Reference("inventory")]
        public IEnumerable<ItemReference<Item>> Inventory { get; set; }

        [Reference("AI Goals")]
        public IEnumerable<ItemReference<AiTask>> AiGoals { get; set; }

        [Reference("personality")]
        public IEnumerable<ItemReference<Personality>> Personality { get; set; }

        [Reference("race")]
        public IEnumerable<ItemReference<Race>> Race { get; set; }

        [Reference("faction")]
        public IEnumerable<ItemReference<Faction>> Faction { get; set; }

        [Reference("shopping")]
        public IEnumerable<ItemReference<Item>> Shopping { get; set; }

        [Reference("weapon level")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponLevel { get; set; }

        [Reference("bounty factions")]
        public IEnumerable<ItemReference<Faction>> BountyFactions { get; set; }

        [Reference("color")]
        public IEnumerable<ItemReference<ColorData>> Color { get; set; }

        [Reference("announcement dialogue")]
        public IEnumerable<ItemReference<Dialogue>> AnnouncementDialogue { get; set; }

        [Reference("crossbows")]
        public IEnumerable<ItemReference<Crossbow>> Crossbows { get; set; }

        [Reference("unique replacement spawn")]
        public IEnumerable<ItemReference<Character>> UniqueReplacementSpawn { get; set; }

        [Reference("death items")]
        public IEnumerable<ItemReference<Item>> DeathItems { get; set; }

        [Reference("starting health")]
        public IEnumerable<ItemReference<LocationalDamage>> StartingHealth { get; set; }

        [Reference("blueprints")]
        public IEnumerable<ItemReference<Research>> Blueprints { get; set; }

        [Reference("vendors")]
        public IEnumerable<ItemReference<VendorList>> Vendors { get; set; }
    }
}