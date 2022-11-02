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
    public class Effect : ItemBase
    {
        public Effect(string stringId, string name)
            : base(stringId, name)
        {
            this.FogVolumes = Enumerable.Empty<ItemReference<EffectFogVolume>>();
            this.Sound = Enumerable.Empty<ItemReference<AmbientSound>>();
        }

        public override ItemType Type => ItemType.Effect;

        [Value("ground colour")]
        public bool? GroundColour { get; set; }

        [Value("wind affected")]
        public bool? WindAffected { get; set; }

        [Value("wind direction emission")]
        public bool? WindDirectionEmission { get; set; }

        [Value("effect radius")]
        public float? EffectRadius { get; set; }

        [Value("effect strength max")]
        public float? EffectStrengthMax { get; set; }

        [Value("effect strength min")]
        public float? EffectStrengthMin { get; set; }

        [Value("fog fade in duration")]
        public float? FogFadeInDuration { get; set; }

        [Value("fog fade out duration")]
        public float? FogFadeOutDuration { get; set; }

        [Value("light fade in delay")]
        public float? LightFadeInDelay { get; set; }

        [Value("light fade in duration")]
        public float? LightFadeInDuration { get; set; }

        [Value("light fade out delay")]
        public float? LightFadeOutDelay { get; set; }

        [Value("light fade out duration")]
        public float? LightFadeOutDuration { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("max time to live")]
        public float? MaxTimeToLive { get; set; }

        [Value("max wind span rate")]
        public float? MaxWindSpanRate { get; set; }

        [Value("maximum view distance")]
        public float? MaximumViewDistance { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min slope")]
        public float? MinSlope { get; set; }

        [Value("min time to live")]
        public float? MinTimeToLive { get; set; }

        [Value("min wind span rate")]
        public float? MinWindSpanRate { get; set; }

        [Value("particle fade out delay")]
        public float? ParticleFadeOutDelay { get; set; }

        [Value("sky colour multiplier")]
        public float? SkyColourMultiplier { get; set; }

        [Value("wandering speed")]
        public float? WanderingSpeed { get; set; }

        [Value("wind speed mult")]
        public float? WindSpeedMult { get; set; }

        [Value("affect type")]
        public int? AffectType { get; set; }

        [Value("colour multiplier")]
        public int? ColourMultiplier { get; set; }

        [Value("type")]
        public int? EffectType { get; set; }

        [Value("particle system")]
        public string? ParticleSystem { get; set; }

        [Value("builds up")]
        public bool? BuildsUp { get; set; }

        [Value("ground effect")]
        public bool? GroundEffect { get; set; }

        [Value("ground particle")]
        public bool? GroundParticle { get; set; }

        [Reference("fog volumes")]
        public IEnumerable<ItemReference<EffectFogVolume>> FogVolumes { get; set; }

        [Reference("sound")]
        public IEnumerable<ItemReference<AmbientSound>> Sound { get; set; }
    }
}