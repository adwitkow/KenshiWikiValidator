using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class FoliageLayer : ItemBase
    {
        public FoliageLayer(string stringId, string name)
            : base(stringId, name)
        {
            this.Meshes = Enumerable.Empty<ItemReference<FoliageMesh>>();
            this.Grass = Enumerable.Empty<ItemReference<Grass>>();
        }

        public override ItemType Type => ItemType.FoliageLayer;

        [Value("wind")]
        public bool? Wind { get; set; }

        [Value("visibility range")]
        public int? VisibilityRange { get; set; }

        [Value("uses foliage system")]
        public bool? UsesFoliageSystem { get; set; }

        [Value("lod levels")]
        public int? LodLevels { get; set; }

        [Value("lod range")]
        public int? LodRange { get; set; }

        [Value("page size")]
        public int? PageSize { get; set; }

        [Reference("meshes")]
        public IEnumerable<ItemReference<FoliageMesh>> Meshes { get; set; }

        [Reference("grass")]
        public IEnumerable<ItemReference<Grass>> Grass { get; set; }

    }
}