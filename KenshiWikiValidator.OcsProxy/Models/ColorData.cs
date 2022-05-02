using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class ColorData : ItemBase
    {
        public ColorData(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.ColorData;

        [Value("color 1")]
        public int? Color1 { get; set; }

        [Value("color 2")]
        public int? Color2 { get; set; }

    }
}