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
    public class BuildingFunctionality : ItemBase
    {
        public BuildingFunctionality(string stringId, string name)
            : base(stringId, name)
        {
            this.Animation = Enumerable.Empty<ItemReference<Animation>>();
            this.Consumes = Enumerable.Empty<ItemReference<Item>>();
            this.Produces = Enumerable.Empty<ItemReference<Item>>();
            this.AnimationEvents = Enumerable.Empty<ItemReference<AnimationEvent>>();
            this.SpecialTool = Enumerable.Empty<ItemReference<Item>>();
            this.AnimationKo = Enumerable.Empty<ItemReference<Animation>>();
            this.AnimationDazed = Enumerable.Empty<ItemReference<Animation>>();
            this.WeaponCrafts = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
            this.ItemCrafts = Enumerable.Empty<ItemReference<Item>>();
            this.ArmourCrafts = Enumerable.Empty<ItemReference<Armour>>();
            this.AnimationLockpick = Enumerable.Empty<ItemReference<Animation>>();
            this.AnimationMedic = Enumerable.Empty<ItemReference<Animation>>();
            this.CrossbowCrafts = Enumerable.Empty<ItemReference<Crossbow>>();
            this.RoboticsCrafts = Enumerable.Empty<ItemReference<LimbReplacement>>();
        }

        public override ItemType Type => ItemType.BuildingFunctionality;

        [Value("has progress bar when used")]
        public bool? HasProgressBarWhenUsed { get; set; }

        [Value("is production building")]
        public bool? IsProductionBuilding { get; set; }

        [Value("occupant selection")]
        public bool? OccupantSelection { get; set; }

        [Value("output per day")]
        public bool? OutputPerDay { get; set; }

        [Value("overrides ingredients")]
        public bool? OverridesIngredients { get; set; }

        [Value("restrains movement")]
        public bool? RestrainsMovement { get; set; }

        [Value("hunger rate")]
        public float? HungerRate { get; set; }

        [Value("production mult")]
        public float? ProductionMult { get; set; }

        [Value("use range")]
        public float? UseRange { get; set; }

        [Value("flags")]
        public int? Flags { get; set; }

        [Value("function")]
        public int? Function { get; set; }

        [Value("max operators")]
        public int? MaxOperators { get; set; }

        [Value("stat used")]
        public int? StatUsed { get; set; }

        [Value("tech level")]
        public int? TechLevel { get; set; }

        [Value("world resource mining")]
        public int? WorldResourceMining { get; set; }

        [Value("string")]
        public string? String { get; set; }

        [Reference("animation")]
        public IEnumerable<ItemReference<Animation>> Animation { get; set; }

        [Reference("consumes")]
        public IEnumerable<ItemReference<Item>> Consumes { get; set; }

        [Reference("produces")]
        public IEnumerable<ItemReference<Item>> Produces { get; set; }

        [Reference("animation events")]
        public IEnumerable<ItemReference<AnimationEvent>> AnimationEvents { get; set; }

        [Reference("special tool")]
        public IEnumerable<ItemReference<Item>> SpecialTool { get; set; }

        [Reference("animation KO")]
        public IEnumerable<ItemReference<Animation>> AnimationKo { get; set; }

        [Reference("animation dazed")]
        public IEnumerable<ItemReference<Animation>> AnimationDazed { get; set; }

        [Reference("weapon crafts")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> WeaponCrafts { get; set; }

        [Reference("item crafts")]
        public IEnumerable<ItemReference<Item>> ItemCrafts { get; set; }

        [Reference("armour crafts")]
        public IEnumerable<ItemReference<Armour>> ArmourCrafts { get; set; }

        [Reference("animation lockpick")]
        public IEnumerable<ItemReference<Animation>> AnimationLockpick { get; set; }

        [Reference("animation medic")]
        public IEnumerable<ItemReference<Animation>> AnimationMedic { get; set; }

        [Reference("crossbow crafts")]
        public IEnumerable<ItemReference<Crossbow>> CrossbowCrafts { get; set; }

        [Reference("robotics crafts")]
        public IEnumerable<ItemReference<LimbReplacement>> RoboticsCrafts { get; set; }
    }
}