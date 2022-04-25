using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialoguePackage : IItem
    {
        public ItemType Type => ItemType.DialoguePackage;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Dialogue> Dialogues { get; set; }
    }
}
