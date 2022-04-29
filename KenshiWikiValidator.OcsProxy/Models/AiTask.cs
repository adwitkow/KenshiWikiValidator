using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class AiTask : ItemBase
    {
        public AiTask(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.AiTask;

        [Value("classification")]
        public int? Classification { get; set; }

        [Value("ending")]
        public int? Ending { get; set; }

        [Value("enum")]
        public int? Enum { get; set; }

        [Value("targeting")]
        public int? Targeting { get; set; }

    }
}