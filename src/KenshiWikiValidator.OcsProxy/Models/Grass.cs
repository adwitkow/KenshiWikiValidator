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
    public class Grass : ItemBase
    {
        public Grass(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Grass;

        [Value("blackout")]
        public bool? Blackout { get; set; }

        [Value("cross quads")]
        public bool? CrossQuads { get; set; }

        [Value("blackout noise scale")]
        public float? BlackoutNoiseScale { get; set; }

        [Value("blackout zero cutoff")]
        public float? BlackoutZeroCutoff { get; set; }

        [Value("brightness boost")]
        public float? BrightnessBoost { get; set; }

        [Value("cap")]
        public float? Cap { get; set; }

        [Value("density")]
        public float? Density { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max height")]
        public float? MaxHeight { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("max width")]
        public float? MaxWidth { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min height")]
        public float? MinHeight { get; set; }

        [Value("min width")]
        public float? MinWidth { get; set; }

        [Value("noise scale")]
        public float? NoiseScale { get; set; }

        [Value("wind factor")]
        public float? WindFactor { get; set; }

        [Value("zero cutoff")]
        public float? ZeroCutoff { get; set; }

        [Value("color map")]
        public object? ColorMap { get; set; }

        [Value("grass sprite")]
        public object? GrassSprite { get; set; }
    }
}