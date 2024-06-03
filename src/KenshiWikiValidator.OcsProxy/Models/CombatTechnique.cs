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
    public class CombatTechnique : ItemBase
    {
        public CombatTechnique(string stringId, string name)
            : base(stringId, name)
        {
            this.Events = Enumerable.Empty<ItemReference<AnimationEvent>>();
        }

        public override ItemType Type => ItemType.CombatTechnique;

        [Value("1 handed")]
        public bool? OneHanded { get; set; }

        [Value("blunt")]
        public bool? Blunt { get; set; }

        [Value("disabled")]
        public bool? Disabled { get; set; }

        [Value("gains ground")]
        public bool? GainsGround { get; set; }

        [Value("hackers")]
        public bool? Hackers { get; set; }

        [Value("heavy weapons")]
        public bool? HeavyWeapons { get; set; }

        [Value("is block")]
        public bool? IsBlock { get; set; }

        [Value("is dodge")]
        public bool? IsDodge { get; set; }

        [Value("is prone")]
        public bool? IsProne { get; set; }

        [Value("is stumble dodge")]
        public bool? IsStumbleDodge { get; set; }

        [Value("katanas")]
        public bool? Katanas { get; set; }

        [Value("low strike")]
        public bool? LowStrike { get; set; }

        [Value("polearm")]
        public bool? Polearm { get; set; }

        [Value("sabre")]
        public bool? Sabre { get; set; }

        [Value("unarmed")]
        public bool? Unarmed { get; set; }

        [Value("use L arm")]
        public bool? UseLArm { get; set; }

        [Value("use R arm")]
        public bool? UseRArm { get; set; }

        [Value("acceptable end time")]
        public float? AcceptableEndTime { get; set; }

        [Value("anim blocked frame 1")]
        public float? AnimBlockedFrame1 { get; set; }

        [Value("anim blocked frame 2")]
        public float? AnimBlockedFrame2 { get; set; }

        [Value("anim hesitate point")]
        public float? AnimHesitatePoint { get; set; }

        [Value("anim speed mult")]
        public float? AnimSpeedMult { get; set; }

        [Value("anim stop frame 1")]
        public float? AnimStopFrame1 { get; set; }

        [Value("anim stop frame 2")]
        public float? AnimStopFrame2 { get; set; }

        [Value("attack distance")]
        public float? AttackDistance { get; set; }

        [Value("attack distance min vs static")]
        public float? AttackDistanceMinVsStatic { get; set; }

        [Value("chance")]
        public float? Chance { get; set; }

        [Value("max encumbrance")]
        public float? MaxEncumbrance { get; set; }

        [Value("max skill")]
        public float? MaxSkill { get; set; }

        [Value("min skill")]
        public float? MinSkill { get; set; }

        [Value("num frames")]
        public float? NumFrames { get; set; }

        [Value("animal")]
        public int? Animal { get; set; }

        [Value("attack direction 1")]
        public int? AttackDirection1 { get; set; }

        [Value("attack direction 2")]
        public int? AttackDirection2 { get; set; }

        [Value("limb 1")]
        public int? Limb1 { get; set; }

        [Value("limb 2")]
        public int? Limb2 { get; set; }

        [Value("max simultaneous hits")]
        public int? MaxSimultaneousHits { get; set; }

        [Value("num techniques")]
        public int? NumTechniques { get; set; }

        [Value("power 1")]
        public int? Power1 { get; set; }

        [Value("power 2")]
        public int? Power2 { get; set; }

        [Value("anim name")]
        public string? AnimName { get; set; }

        [Reference("events")]
        public IEnumerable<ItemReference<AnimationEvent>> Events { get; set; }
    }
}