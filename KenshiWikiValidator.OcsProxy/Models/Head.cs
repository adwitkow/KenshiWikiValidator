using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Head : ItemBase
    {
        public Head(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Head;

        [Value("playable")]
        public bool? Playable { get; set; }

        [Value("mask map")]
        public object? MaskMap { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

    }
}