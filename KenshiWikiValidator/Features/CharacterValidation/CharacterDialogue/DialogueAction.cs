using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialogueAction : IDataItem
    {
        public ItemType Type => ItemType.DialogAction;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }
    }
}
