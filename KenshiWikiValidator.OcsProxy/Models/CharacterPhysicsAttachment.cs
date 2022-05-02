using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class CharacterPhysicsAttachment : ItemBase
    {
        public CharacterPhysicsAttachment(string stringId, string name)
            : base(stringId, name)
        {
            this.LightData = Enumerable.Empty<ItemReference<Light>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
        }

        public override ItemType Type => ItemType.CharacterPhysicsAttachment;

        [Value("bone name")]
        public string? BoneName { get; set; }

        [Value("file female")]
        public object? FileFemale { get; set; }

        [Value("file male")]
        public object? FileMale { get; set; }

        [Reference("light data")]
        public IEnumerable<ItemReference<Light>> LightData { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }

    }
}