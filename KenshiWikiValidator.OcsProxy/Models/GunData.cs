using OpenConstructionSet.Models;

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