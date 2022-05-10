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
    public class Constants : ItemBase
    {
        public Constants(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Constants;

        [Value("damage resistance randomised")]
        public bool? DamageResistanceRandomised { get; set; }

        [Value("animation blend rate")]
        public float? AnimationBlendRate { get; set; }

        [Value("appearance random deviation percentage")]
        public float? AppearanceRandomDeviationPercentage { get; set; }

        [Value("armor price mult")]
        public float? ArmorPriceMult { get; set; }

        [Value("attack chance factor")]
        public float? AttackChanceFactor { get; set; }

        [Value("base block chance")]
        public float? BaseBlockChance { get; set; }

        [Value("bed hunger rate")]
        public float? BedHungerRate { get; set; }

        [Value("bleed rate")]
        public float? BleedRate { get; set; }

        [Value("bleeding clot rate")]
        public float? BleedingClotRate { get; set; }

        [Value("block chance increase per 10levels")]
        public float? BlockChanceIncreasePer10levels { get; set; }

        [Value("block chance reduction per 10levels")]
        public float? BlockChanceReductionPer10levels { get; set; }

        [Value("blood recovery rate")]
        public float? BloodRecoveryRate { get; set; }

        [Value("blueprint price mult")]
        public float? BlueprintPriceMult { get; set; }

        [Value("blunt damage 1")]
        public float? BluntDamage1 { get; set; }

        [Value("blunt damage 99")]
        public float? BluntDamage99 { get; set; }

        [Value("blunt permanent organ damage")]
        public float? BluntPermanentOrganDamage { get; set; }

        [Value("bodypart degeneration rate")]
        public float? BodypartDegenerationRate { get; set; }

        [Value("bow damage 1")]
        public float? BowDamage1 { get; set; }

        [Value("bow damage 99")]
        public float? BowDamage99 { get; set; }

        [Value("build speed")]
        public float? BuildSpeed { get; set; }

        [Value("carry person weight")]
        public float? CarryPersonWeight { get; set; }

        [Value("carry weight mult")]
        public float? CarryWeightMult { get; set; }

        [Value("clothing price mult")]
        public float? ClothingPriceMult { get; set; }

        [Value("crossbow price mult")]
        public float? CrossbowPriceMult { get; set; }

        [Value("cut damage 1")]
        public float? CutDamage1 { get; set; }

        [Value("cut damage 99")]
        public float? CutDamage99 { get; set; }

        [Value("damage multiplier")]
        public float? DamageMultiplier { get; set; }

        [Value("damage resistance max")]
        public float? DamageResistanceMax { get; set; }

        [Value("damage resistance min")]
        public float? DamageResistanceMin { get; set; }

        [Value("degeneration mult 1")]
        public float? DegenerationMult1 { get; set; }

        [Value("degeneration mult 99")]
        public float? DegenerationMult99 { get; set; }

        [Value("encumbrance base")]
        public float? EncumbranceBase { get; set; }

        [Value("encumbrance hunger rate")]
        public float? EncumbranceHungerRate { get; set; }

        [Value("exp gain multiplier")]
        public float? ExpGainMultiplier { get; set; }

        [Value("exposure max")]
        public float? ExposureMax { get; set; }

        [Value("exposure min")]
        public float? ExposureMin { get; set; }

        [Value("extra blood loss from bodyparts")]
        public float? ExtraBloodLossFromBodyparts { get; set; }

        [Value("fed recovery rate mult")]
        public float? FedRecoveryRateMult { get; set; }

        [Value("food price mult")]
        public float? FoodPriceMult { get; set; }

        [Value("food quality mult")]
        public float? FoodQualityMult { get; set; }

        [Value("global price mult")]
        public float? GlobalPriceMult { get; set; }

        [Value("heal rate mult")]
        public float? HealRateMult { get; set; }

        [Value("immediate blood loss")]
        public float? ImmediateBloodLoss { get; set; }

        [Value("knockout mult 1")]
        public float? KnockoutMult1 { get; set; }

        [Value("knockout mult 99")]
        public float? KnockoutMult99 { get; set; }

        [Value("knockout time base")]
        public float? KnockoutTimeBase { get; set; }

        [Value("latitude")]
        public float? Latitude { get; set; }

        [Value("loot price mult")]
        public float? LootPriceMult { get; set; }

        [Value("loot price mult GEAR")]
        public float? LootPriceMultGear { get; set; }

        [Value("loot price mult player armour")]
        public float? LootPriceMultPlayerArmour { get; set; }

        [Value("loot price mult player weapons")]
        public float? LootPriceMultPlayerWeapons { get; set; }

        [Value("max toughness ko point")]
        public float? MaxToughnessKoPoint { get; set; }

        [Value("medic speed mult")]
        public float? MedicSpeedMult { get; set; }

        [Value("medkit drain 1")]
        public float? MedkitDrain1 { get; set; }

        [Value("medkit drain 99")]
        public float? MedkitDrain99 { get; set; }

        [Value("min dismantle materials percentage")]
        public float? MinDismantleMaterialsPercentage { get; set; }

        [Value("min strength xp mult")]
        public float? MinStrengthXpMult { get; set; }

        [Value("min toughness ko point")]
        public float? MinToughnessKoPoint { get; set; }

        [Value("minimum lockpick chance")]
        public float? MinimumLockpickChance { get; set; }

        [Value("night darkness")]
        public float? NightDarkness { get; set; }

        [Value("pierce damage multiplier")]
        public float? PierceDamageMultiplier { get; set; }

        [Value("prison time")]
        public float? PrisonTime { get; set; }

        [Value("production speed")]
        public float? ProductionSpeed { get; set; }

        [Value("research level increase rate")]
        public float? ResearchLevelIncreaseRate { get; set; }

        [Value("research rate")]
        public float? ResearchRate { get; set; }

        [Value("resting heal rate mult")]
        public float? RestingHealRateMult { get; set; }

        [Value("robot medic speed mult")]
        public float? RobotMedicSpeedMult { get; set; }

        [Value("robot wear rate")]
        public float? RobotWearRate { get; set; }

        [Value("robotics price mult")]
        public float? RoboticsPriceMult { get; set; }

        [Value("skill diff xp 0x penalty")]
        public float? SkillDiffXp0xPenalty { get; set; }

        [Value("skill diff xp 2x bonus")]
        public float? SkillDiffXp2xBonus { get; set; }

        [Value("starvation time")]
        public float? StarvationTime { get; set; }

        [Value("stun recovery rate")]
        public float? StunRecoveryRate { get; set; }

        [Value("sunrise")]
        public float? Sunrise { get; set; }

        [Value("sunset")]
        public float? Sunset { get; set; }

        [Value("sword price mult")]
        public float? SwordPriceMult { get; set; }

        [Value("trade price mult")]
        public float? TradePriceMult { get; set; }

        [Value("trade profit margins")]
        public float? TradeProfitMargins { get; set; }

        [Value("unarmed damage mult")]
        public float? UnarmedDamageMult { get; set; }

        [Value("weapon inventory weight mult")]
        public float? WeaponInventoryWeightMult { get; set; }

        [Value("weight strength diff max")]
        public float? WeightStrengthDiffMax { get; set; }

        [Value("xp rate athletics")]
        public float? XpRateAthletics { get; set; }

        [Value("XP rate medic 1")]
        public float? XpRateMedic1 { get; set; }

        [Value("XP rate medic 99")]
        public float? XpRateMedic99 { get; set; }

        [Value("xp rate strength")]
        public float? XpRateStrength { get; set; }

        [Value("xp rate strength from walking")]
        public float? XpRateStrengthFromWalking { get; set; }

        [Value("xp rate toughness")]
        public float? XpRateToughness { get; set; }

        [Value("days per year")]
        public int? DaysPerYear { get; set; }

        [Value("max faction size")]
        public int? MaxFactionSize { get; set; }

        [Value("max num attack slots")]
        public int? MaxNumAttackSlots { get; set; }

        [Value("max squad size")]
        public int? MaxSquadSize { get; set; }

        [Value("max squads")]
        public int? MaxSquads { get; set; }

        [Value("stumble damage max")]
        public int? StumbleDamageMax { get; set; }

        [Value("weight strength diff 1x")]
        public int? WeightStrengthDiff1x { get; set; }

        [Value("female skeleton file")]
        public object? FemaleSkeletonFile { get; set; }

        [Value("male skeleton file")]
        public object? MaleSkeletonFile { get; set; }
    }
}