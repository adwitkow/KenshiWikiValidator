using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class FoliageBuilding : ItemBase
    {
        public FoliageBuilding(string stringId, string name)
            : base(stringId, name)
        {
            this.Building = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.FoliageBuilding;

        [Value("clustered")]
        public bool? Clustered { get; set; }

        [Value("keep upright")]
        public bool? KeepUpright { get; set; }

        [Value("limit to grass areas")]
        public bool? LimitToGrassAreas { get; set; }

        [Value("slope align")]
        public bool? SlopeAlign { get; set; }

        [Value("use accurate trace")]
        public bool? UseAccurateTrace { get; set; }

        [Value("child cluster radius")]
        public float? ChildClusterRadius { get; set; }

        [Value("cluster radius max")]
        public float? ClusterRadiusMax { get; set; }

        [Value("cluster radius min")]
        public float? ClusterRadiusMin { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min slope")]
        public float? MinSlope { get; set; }

        [Value("vertical offset max")]
        public float? VerticalOffsetMax { get; set; }

        [Value("vertical offset min")]
        public float? VerticalOffsetMin { get; set; }

        [Value("wind factor")]
        public float? WindFactor { get; set; }

        [Value("cluster num max")]
        public int? ClusterNumMax { get; set; }

        [Value("cluster num min")]
        public int? ClusterNumMin { get; set; }

        [Reference("building")]
        public IEnumerable<ItemReference<Building>> Building { get; set; }

    }
}