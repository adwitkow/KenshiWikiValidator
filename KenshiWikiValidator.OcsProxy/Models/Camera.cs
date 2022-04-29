using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Camera : ItemBase
    {
        public Camera(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Camera;

        [Value("grass")]
        public bool? Grass { get; set; }

        [Value("density")]
        public float? Density { get; set; }

        [Value("max height")]
        public float? MaxHeight { get; set; }

        [Value("max width")]
        public float? MaxWidth { get; set; }

        [Value("min height")]
        public float? MinHeight { get; set; }

        [Value("min width")]
        public float? MinWidth { get; set; }

        [Value("lod range")]
        public int? LodRange { get; set; }

        [Value("Collision")]
        public object? Collision { get; set; }

        [Value("color map")]
        public object? ColorMap { get; set; }

        [Value("Mesh")]
        public object? Mesh { get; set; }

    }
}