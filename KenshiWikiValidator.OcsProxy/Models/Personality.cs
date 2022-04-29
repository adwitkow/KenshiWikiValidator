using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Personality : ItemBase
    {
        public Personality(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Personality;

        [Value("tags always0")]
        public int? TagsAlways0 { get; set; }

        [Value("tags common0")]
        public int? TagsCommon0 { get; set; }

        [Value("tags never0")]
        public int? TagsNever0 { get; set; }

        [Value("tags rare0")]
        public int? TagsRare0 { get; set; }

        [Value("tags never1")]
        public int? TagsNever1 { get; set; }

        [Value("tags never2")]
        public int? TagsNever2 { get; set; }

        [Value("tags never3")]
        public int? TagsNever3 { get; set; }

        [Value("tags common1")]
        public int? TagsCommon1 { get; set; }

        [Value("tags common2")]
        public int? TagsCommon2 { get; set; }

        [Value("tags common3")]
        public int? TagsCommon3 { get; set; }

        [Value("tags common4")]
        public int? TagsCommon4 { get; set; }

        [Value("tags common5")]
        public int? TagsCommon5 { get; set; }

        [Value("tags rare1")]
        public int? TagsRare1 { get; set; }

        [Value("tags common6")]
        public int? TagsCommon6 { get; set; }

        [Value("tags rare2")]
        public int? TagsRare2 { get; set; }

        [Value("tags always1")]
        public int? TagsAlways1 { get; set; }

        [Value("tags always2")]
        public int? TagsAlways2 { get; set; }

    }
}