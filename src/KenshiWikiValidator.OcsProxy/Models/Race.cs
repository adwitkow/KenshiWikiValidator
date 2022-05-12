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
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Race : ItemBase, IDescriptive
    {
        public Race(string stringId, string name)
            : base(stringId, name)
        {
            this.AiGoals = Enumerable.Empty<ItemReference<AiTask>>();
            this.CombatAnatomy = Enumerable.Empty<ItemReference<LocationalDamage>>();
            this.Hairs = Enumerable.Empty<ItemReference<Attachment>>();
            this.HeadsFemale = Enumerable.Empty<ItemReference<Head>>();
            this.HeadsMale = Enumerable.Empty<ItemReference<Head>>();
            this.FistsLv1 = Enumerable.Empty<ItemReference<Weapon>>();
            this.FistsLv100 = Enumerable.Empty<ItemReference<Weapon>>();
            this.HairColors = Enumerable.Empty<ItemReference<ColorData>>();
            this.LimbReplacement = Enumerable.Empty<ItemReference<LimbReplacement>>();
            this.SeveredLimbs = Enumerable.Empty<ItemReference<Item>>();
            this.SpecialFood = Enumerable.Empty<ItemReference<Item>>();
            this.Backpacks = Enumerable.Empty<ItemReference<Container>>();
            this.AmbientSound = Enumerable.Empty<ItemReference<AmbientSound>>();
        }

        public override ItemType Type => ItemType.Race;

        [Value("barefoot")]
        public bool? Barefoot { get; set; }

        [Value("cant enter buildings")]
        public bool? CantEnterBuildings { get; set; }

        [Value("carriable")]
        public bool? Carriable { get; set; }

        [Value("gigantic")]
        public bool? Gigantic { get; set; }

        [Value("is robot")]
        public bool? IsRobot { get; set; }

        [Value("no hats")]
        public bool? NoHats { get; set; }

        [Value("no shirts")]
        public bool? NoShirts { get; set; }

        [Value("no shoes")]
        public bool? NoShoes { get; set; }

        [Value("playable")]
        public bool? Playable { get; set; }

        [Value("self healing")]
        public bool? SelfHealing { get; set; }

        [Value("single gender")]
        public bool? SingleGender { get; set; }

        [Value("swims")]
        public bool? Swims { get; set; }

        [Value("vampiric")]
        public bool? Vampiric { get; set; }

        [Value("bleed rate")]
        public float? BleedRate { get; set; }

        [Value("blood vertical")]
        public float? BloodVertical { get; set; }

        [Value("combat move speed mult")]
        public float? CombatMoveSpeedMult { get; set; }

        [Value("dexterity")]
        public float? Dexterity { get; set; }

        [Value("heal rate")]
        public float? HealRate { get; set; }

        [Value("hull size X")]
        public float? HullSizeX { get; set; }

        [Value("hull size Y")]
        public float? HullSizeY { get; set; }

        [Value("hull size Z")]
        public float? HullSizeZ { get; set; }

        [Value("hunger rate")]
        public float? HungerRate { get; set; }

        [Value("max blood")]
        public float? MaxBlood { get; set; }

        [Value("min blood")]
        public float? MinBlood { get; set; }

        [Value("pathfind acceleration")]
        public float? PathfindAcceleration { get; set; }

        [Value("pathfind footprint radius")]
        public float? PathfindFootprintRadius { get; set; }

        [Value("portrait distance")]
        public float? PortraitDistance { get; set; }

        [Value("portrait offset x")]
        public float? PortraitOffsetX { get; set; }

        [Value("portrait offset y")]
        public float? PortraitOffsetY { get; set; }

        [Value("portrait offset z")]
        public float? PortraitOffsetZ { get; set; }

        [Value("strength")]
        public float? Strength { get; set; }

        [Value("swim offset")]
        public float? SwimOffset { get; set; }

        [Value("swim speed mult")]
        public float? SwimSpeedMult { get; set; }

        [Value("vision range mult")]
        public float? VisionRangeMult { get; set; }

        [Value("walk speed")]
        public float? WalkSpeed { get; set; }

        [Value("water avoidance")]
        public float? WaterAvoidance { get; set; }

        [Value("blood colour")]
        public int? BloodColour { get; set; }

        [Value("blood horizontal")]
        public int? BloodHorizontal { get; set; }

        [Value("buy value")]
        public int? BuyValue { get; set; }

        [Value("extra attack slots")]
        public int? ExtraAttackSlots { get; set; }

        [Value("heal stat")]
        public int? HealStat { get; set; }

        [Value("morph num")]
        public int? MorphNum { get; set; }

        [Value("sounds")]
        public int? Sounds { get; set; }

        [Value("speed max skill")]
        public int? SpeedMaxSkill { get; set; }

        [Value("speed min skill")]
        public int? SpeedMinSkill { get; set; }

        [Value("stats bad0")]
        public int? StatsBad0 { get; set; }

        [Value("stats good0")]
        public int? StatsGood0 { get; set; }

        [Value("stats good1")]
        public int? StatsGood1 { get; set; }

        [Value("weather immunity0")]
        public int? WeatherImmunity0 { get; set; }

        [Value("weather immunity1")]
        public int? WeatherImmunity1 { get; set; }

        [Value("carry bone")]
        public string? CarryBone { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("filenames prefix")]
        public string? FilenamesPrefix { get; set; }

        [Value("attachment points")]
        public object? AttachmentPoints { get; set; }

        [Value("body mask female")]
        public object? BodyMaskFemale { get; set; }

        [Value("body mask male")]
        public object? BodyMaskMale { get; set; }

        [Value("body texture female")]
        public object? BodyTextureFemale { get; set; }

        [Value("body texture male")]
        public object? BodyTextureMale { get; set; }

        [Value("editor limits")]
        public object? EditorLimits { get; set; }

        [Value("female carry ragdoll")]
        public object? FemaleCarryRagdoll { get; set; }

        [Value("female mesh")]
        public object? FemaleMesh { get; set; }

        [Value("female ragdoll")]
        public object? FemaleRagdoll { get; set; }

        [Value("flayed normal female")]
        public object? FlayedNormalFemale { get; set; }

        [Value("flayed normal male")]
        public object? FlayedNormalMale { get; set; }

        [Value("flayed texture female")]
        public object? FlayedTextureFemale { get; set; }

        [Value("flayed texture male")]
        public object? FlayedTextureMale { get; set; }

        [Value("male carry ragdoll")]
        public object? MaleCarryRagdoll { get; set; }

        [Value("male mesh")]
        public object? MaleMesh { get; set; }

        [Value("male ragdoll")]
        public object? MaleRagdoll { get; set; }

        [Value("nm female")]
        public object? NmFemale { get; set; }

        [Value("nm female skinny")]
        public object? NmFemaleSkinny { get; set; }

        [Value("nm female strong")]
        public object? NmFemaleStrong { get; set; }

        [Value("nm male")]
        public object? NmMale { get; set; }

        [Value("nm male skinny")]
        public object? NmMaleSkinny { get; set; }

        [Value("nm male strong")]
        public object? NmMaleStrong { get; set; }

        [Value("part map female")]
        public object? PartMapFemale { get; set; }

        [Value("part map male")]
        public object? PartMapMale { get; set; }

        [Value("stats bad1")]
        public int? StatsBad1 { get; set; }

        [Value("stats bad2")]
        public int? StatsBad2 { get; set; }

        [Value("stats bad3")]
        public int? StatsBad3 { get; set; }

        [Value("stats bad4")]
        public int? StatsBad4 { get; set; }

        [Value("stats good2")]
        public int? StatsGood2 { get; set; }

        [Value("stats good3")]
        public int? StatsGood3 { get; set; }

        [Value("stats good4")]
        public int? StatsGood4 { get; set; }

        [Value("swim speed")]
        public int? SwimSpeed { get; set; }

        [Value("weather immunity2")]
        public int? WeatherImmunity2 { get; set; }

        [Value("weather immunity3")]
        public int? WeatherImmunity3 { get; set; }

        [Value("stats good5")]
        public int? StatsGood5 { get; set; }

        [Value("stats good6")]
        public int? StatsGood6 { get; set; }

        [Value("stats good7")]
        public int? StatsGood7 { get; set; }

        [Value("stats bad5")]
        public int? StatsBad5 { get; set; }

        [Value("stats bad6")]
        public int? StatsBad6 { get; set; }

        [Value("stats bad7")]
        public int? StatsBad7 { get; set; }

        [Value("stats bad8")]
        public int? StatsBad8 { get; set; }

        [Value("stats bad9")]
        public int? StatsBad9 { get; set; }

        [Value("weather immunity4")]
        public int? WeatherImmunity4 { get; set; }

        [Reference("AI Goals")]
        public IEnumerable<ItemReference<AiTask>> AiGoals { get; set; }

        [Reference("combat anatomy")]
        public IEnumerable<ItemReference<LocationalDamage>> CombatAnatomy { get; set; }

        [Reference("hairs")]
        public IEnumerable<ItemReference<Attachment>> Hairs { get; set; }

        [Reference("heads female")]
        public IEnumerable<ItemReference<Head>> HeadsFemale { get; set; }

        [Reference("heads male")]
        public IEnumerable<ItemReference<Head>> HeadsMale { get; set; }

        [Reference("fists lv1")]
        public IEnumerable<ItemReference<Weapon>> FistsLv1 { get; set; }

        [Reference("fists lv100")]
        public IEnumerable<ItemReference<Weapon>> FistsLv100 { get; set; }

        [Reference("hair colors")]
        public IEnumerable<ItemReference<ColorData>> HairColors { get; set; }

        [Reference("limb replacement")]
        public IEnumerable<ItemReference<LimbReplacement>> LimbReplacement { get; set; }

        [Reference("severed limbs")]
        public IEnumerable<ItemReference<Item>> SeveredLimbs { get; set; }

        [Reference("special food")]
        public IEnumerable<ItemReference<Item>> SpecialFood { get; set; }

        [Reference("backpacks")]
        public IEnumerable<ItemReference<Container>> Backpacks { get; set; }

        [Reference("ambient sound")]
        public IEnumerable<ItemReference<AmbientSound>> AmbientSound { get; set; }
    }
}