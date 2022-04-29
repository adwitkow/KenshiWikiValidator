using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Road : ItemBase
    {
        public Road(string stringId, string name)
            : base(stringId, name)
        {
            this.Spawns = Enumerable.Empty<ItemReference<SquadTemplate>>();
        }

        public override ItemType Type => ItemType.Road;

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("population amount")]
        public float? PopulationAmount { get; set; }

        [Value("color channel")]
        public int? ColorChannel { get; set; }

        [Value("spawns max")]
        public int? SpawnsMax { get; set; }

        [Value("spawns min")]
        public int? SpawnsMin { get; set; }

        [Value("imagefile")]
        public object? Imagefile { get; set; }

        [Reference("spawns")]
        public IEnumerable<ItemReference<SquadTemplate>> Spawns { get; set; }

    }
}