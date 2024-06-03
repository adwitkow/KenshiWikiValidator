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
    public class AnimalAnimation : ItemBase
    {
        public AnimalAnimation(string stringId, string name)
            : base(stringId, name)
        {
            this.Events = Enumerable.Empty<ItemReference<AnimationEvent>>();
        }

        public override ItemType Type => ItemType.AnimalAnimation;

        [Value("being carried")]
        public bool? BeingCarried { get; set; }

        [Value("big stumble")]
        public bool? BigStumble { get; set; }

        [Value("carrying left")]
        public bool? CarryingLeft { get; set; }

        [Value("carrying right")]
        public bool? CarryingRight { get; set; }

        [Value("delete root")]
        public bool? DeleteRoot { get; set; }

        [Value("disables movement")]
        public bool? DisablesMovement { get; set; }

        [Value("idle")]
        public bool? Idle { get; set; }

        [Value("loop")]
        public bool? Loop { get; set; }

        [Value("normalise")]
        public bool? Normalise { get; set; }

        [Value("relocates")]
        public bool? Relocates { get; set; }

        [Value("reverse looping")]
        public bool? ReverseLooping { get; set; }

        [Value("synchs")]
        public bool? Synchs { get; set; }

        [Value("max speed")]
        public float? MaxSpeed { get; set; }

        [Value("min speed")]
        public float? MinSpeed { get; set; }

        [Value("move speed")]
        public float? MoveSpeed { get; set; }

        [Value("play speed")]
        public float? PlaySpeed { get; set; }

        [Value("strafe speed max")]
        public float? StrafeSpeedMax { get; set; }

        [Value("synch offset")]
        public float? SynchOffset { get; set; }

        [Value("chance")]
        public int? Chance { get; set; }

        [Value("idle chance")]
        public int? IdleChance { get; set; }

        [Value("idle time max")]
        public int? IdleTimeMax { get; set; }

        [Value("idle time min")]
        public int? IdleTimeMin { get; set; }

        [Value("is combat mode")]
        public int? IsCombatMode { get; set; }

        [Value("stealth mode")]
        public int? StealthMode { get; set; }

        [Value("stumble from")]
        public int? StumbleFrom { get; set; }

        [Value("anim name")]
        public string? AnimName { get; set; }

        [Value("layer")]
        public string? Layer { get; set; }

        [Reference("events")]
        public IEnumerable<ItemReference<AnimationEvent>> Events { get; set; }
    }
}