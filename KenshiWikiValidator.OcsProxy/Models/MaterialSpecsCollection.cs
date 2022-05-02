using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class MaterialSpecsCollection : ItemBase
    {
        public MaterialSpecsCollection(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
        }

        public override ItemType Type => ItemType.MaterialSpecsCollection;

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

    }
}