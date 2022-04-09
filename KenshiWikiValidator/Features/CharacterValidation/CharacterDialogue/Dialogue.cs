using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class Dialogue : IItem
    {
        public ItemType Type => ItemType.Dialogue;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<DialogueLine> Lines { get; set; }

        public IEnumerable<DialogueCondition> Conditions { get; set; }

        public IEnumerable<DataItem> WorldStates { get; set; }

        public IEnumerable<DataItem> TargetItems { get; set; }

        public IEnumerable<DataItem> TargetFactions { get; set; }

        public IEnumerable<DataItem> TargetRaces { get; set; }

        public IEnumerable<DataItem> SpeakerIsCharacter { get; set; }

        public IEnumerable<DataItem> InTownOfFactions { get; set; }

        public IEnumerable<DialogueEvent> Events { get; set; }
    }
}
