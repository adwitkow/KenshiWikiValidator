using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class FarmPart : ItemBase
    {
        public FarmPart(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
        }

        public override ItemType Type => ItemType.FarmPart;

        [Value("delay")]
        public float? Delay { get; set; }

        [Value("offset end")]
        public float? OffsetEnd { get; set; }

        [Value("offset start")]
        public float? OffsetStart { get; set; }

        [Value("scale end")]
        public float? ScaleEnd { get; set; }

        [Value("scale start")]
        public float? ScaleStart { get; set; }

        [Value("scale variance")]
        public float? ScaleVariance { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

    }
}