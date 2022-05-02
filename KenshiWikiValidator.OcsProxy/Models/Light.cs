using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Light : ItemBase
    {
        public Light(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Light;

        [Value("buildings")]
        public bool? Buildings { get; set; }

        [Value("characters")]
        public bool? Characters { get; set; }

        [Value("landscape")]
        public bool? Landscape { get; set; }

        [Value("brightness")]
        public float? Brightness { get; set; }

        [Value("falloff")]
        public float? Falloff { get; set; }

        [Value("inner")]
        public float? Inner { get; set; }

        [Value("outer")]
        public float? Outer { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("variance")]
        public float? Variance { get; set; }

        [Value("diffuse")]
        public int? Diffuse { get; set; }

        [Value("effect")]
        public int? Effect { get; set; }

        [Value("type")]
        public int? LightType { get; set; }

        [Value("specular")]
        public int? Specular { get; set; }

    }
}