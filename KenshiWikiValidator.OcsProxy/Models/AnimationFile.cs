using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AnimationFile : ItemBase
    {
        public AnimationFile(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.AnimationFile;

        [Value("preprocess")]
        public bool? Preprocess { get; set; }

        [Value("female animation")]
        public object? FemaleAnimation { get; set; }

        [Value("male animation")]
        public object? MaleAnimation { get; set; }

    }
}