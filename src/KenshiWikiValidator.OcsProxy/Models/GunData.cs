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
    public class GunData : ItemBase
    {
        public GunData(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.GunData;

        [Value("accuracy deviation at 0 skill")]
        public float? AccuracyDeviationAt0Skill { get; set; }

        [Value("aim speed")]
        public float? AimSpeed { get; set; }

        [Value("barrel pos Y")]
        public float? BarrelPosY { get; set; }

        [Value("barrel pos Z")]
        public float? BarrelPosZ { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("reload time max")]
        public float? ReloadTimeMax { get; set; }

        [Value("reload time min")]
        public float? ReloadTimeMin { get; set; }

        [Value("shot speed")]
        public float? ShotSpeed { get; set; }

        [Value("accuracy perfect skill")]
        public int? AccuracyPerfectSkill { get; set; }

        [Value("num shots")]
        public int? NumShots { get; set; }

        [Value("range")]
        public int? Range { get; set; }

        [Value("shoot sound")]
        public int? ShootSound { get; set; }

        [Value("extra mesh loaded")]
        public object? ExtraMeshLoaded { get; set; }

        [Value("extra mesh unloaded")]
        public object? ExtraMeshUnloaded { get; set; }

        [Value("live ammo")]
        public object? LiveAmmo { get; set; }
    }
}