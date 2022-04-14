using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialoguePackage : IDataItem
    {
        public ItemType Type => ItemType.DialoguePackage;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Dialogue> Dialogues { get; set; }
    }
}
