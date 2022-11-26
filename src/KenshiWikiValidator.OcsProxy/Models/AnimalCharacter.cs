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

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AnimalCharacter : ItemBase, IStatsContainer
    {
        public AnimalCharacter(string stringId, string name)
            : base(stringId, name)
        {
            this.AiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.Animations = Enumerable.Empty<ItemReference<AnimalAnimation>>();
            this.Race = Enumerable.Empty<ItemReference<Race>>();
            this.StrafeAnim = Enumerable.Empty<ItemReference<AnimalAnimation>>();
            this.Weapon = Enumerable.Empty<ItemReference<Weapon>>();
            this.WeaponLevel = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.DeathItems = Enumerable.Empty<ItemReference<Item>>();
            this.TurningAnim = Enumerable.Empty<ItemReference<AnimalAnimation>>();
            this.BirdAttractor = Enumerable.Empty<ItemReference<WildlifeBirds>>();
            this.Stats = Enumerable.Empty<ItemReference<Stats>>();
            this.Backpack = Enumerable.Empty<ItemReference<Container>>();
            this.Faction = Enumerable.Empty<ItemReference<Faction>>();
        }

        public override ItemType Type => ItemType.AnimalCharacter;

        [Value("assigns bounties")]
        public bool? AssignsBounties { get; set; }

        [Value("unique")]
        public bool? Unique { get; set; }

        [Value("faction importance")]
        public float? FactionImportance { get; set; }

        [Value("HP mult")]
        public float? HpMult { get; set; }

        [Value("lifespan")]
        public float? Lifespan { get; set; }

        [Value("scale max")]
        public float? ScaleMax { get; set; }

        [Value("scale min")]
        public float? ScaleMin { get; set; }

        [Value("smell blood")]
        public float? SmellBlood { get; set; }

        [Value("smell eggs")]
        public float? SmellEggs { get; set; }

        [Value("turn rate")]
        public float? TurnRate { get; set; }

        [Value("animal strength")]
        public int? AnimalStrength { get; set; }

        [Value("attack type")]
        public int? AttackType { get; set; }

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

        [Value("inventory h")]
        public int? InventoryH { get; set; }

        [Value("inventory w")]
        public int? InventoryW { get; set; }

        [Value("ranged stats")]
        public int? RangedStats { get; set; }

        [Value("sounds")]
        public int? Sounds { get; set; }

        [Value("stats randomise")]
        public int? StatsRandomise { get; set; }

        [Value("stealth stats")]
        public int? StealthStats { get; set; }

        [Value("strength")]
        public int? Strength { get; set; }

        [Value("unarmed stats")]
        public int? UnarmedStats { get; set; }

        [Reference("AI Goals")]
        public IEnumerable<ItemReference<AiTask>> AiGoals { get; set; }

        [Reference("animations")]
        public IEnumerable<ItemReference<AnimalAnimation>> Animations { get; set; }

        [Reference("race")]
        public IEnumerable<ItemReference<Race>> Race { get; set; }

        [Reference("strafe anim")]
        public IEnumerable<ItemReference<AnimalAnimation>> StrafeAnim { get; set; }

        [Reference("weapon")]
        public IEnumerable<ItemReference<Weapon>> Weapon { get; set; }

        [Reference("weapon level")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponLevel { get; set; }

        [Reference("death items")]
        public IEnumerable<ItemReference<Item>> DeathItems { get; set; }

        [Reference("turning anim")]
        public IEnumerable<ItemReference<AnimalAnimation>> TurningAnim { get; set; }

        [Reference("bird attractor")]
        public IEnumerable<ItemReference<WildlifeBirds>> BirdAttractor { get; set; }

        [Reference("stats")]
        public IEnumerable<ItemReference<Stats>> Stats { get; set; }

        [Reference("backpack")]
        public IEnumerable<ItemReference<Container>> Backpack { get; set; }

        [Reference("faction")]
        public IEnumerable<ItemReference<Faction>> Faction { get; set; }
    }
}