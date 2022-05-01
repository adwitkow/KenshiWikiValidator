using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class BiomeGroup : ItemBase
    {
        public BiomeGroup(string stringId, string name)
            : base(stringId, name)
        {
            this.ArrivalDialog = Enumerable.Empty<ItemReference<Dialogue>>();
            this.Resources = Enumerable.Empty<ItemReference<EnvironmentResources>>();
            this.HomelessSpawns = Enumerable.Empty<ItemReference<Squad>>();
            this.Nests = Enumerable.Empty<ItemReference<Town>>();
            this.Seasons = Enumerable.Empty<ItemReference<Season>>();
            this.Birds = Enumerable.Empty<ItemReference<WildlifeBirds>>();
        }

        public override ItemType Type => ItemType.BiomeGroup;

        [Value("nests at fixed markers only")]
        public bool? NestsAtFixedMarkersOnly { get; set; }

        [Value("acidic ground")]
        public float? AcidicGround { get; set; }

        [Value("acidic water")]
        public float? AcidicWater { get; set; }

        [Value("farm water usage")]
        public float? FarmWaterUsage { get; set; }

        [Value("homeless spawn amount")]
        public float? HomelessSpawnAmount { get; set; }

        [Value("weather strength multiplier max")]
        public float? WeatherStrengthMultiplierMax { get; set; }

        [Value("weather strength multiplier min")]
        public float? WeatherStrengthMultiplierMin { get; set; }

        [Value("ambience")]
        public int? Ambience { get; set; }

        [Value("index")]
        public int? Index { get; set; }

        [Value("music")]
        public int? Music { get; set; }

        [Value("num nests")]
        public int? NumNests { get; set; }

        [Reference("arrival dialog")]
        public IEnumerable<ItemReference<Dialogue>> ArrivalDialog { get; set; }

        [Reference("resources")]
        public IEnumerable<ItemReference<EnvironmentResources>> Resources { get; set; }

        [Reference("homeless spawns")]
        public IEnumerable<ItemReference<Squad>> HomelessSpawns { get; set; }

        [Reference("nests")]
        public IEnumerable<ItemReference<Town>> Nests { get; set; }

        [Reference("seasons")]
        public IEnumerable<ItemReference<Season>> Seasons { get; set; }

        [Reference("birds")]
        public IEnumerable<ItemReference<WildlifeBirds>> Birds { get; set; }

    }
}