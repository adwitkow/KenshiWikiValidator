using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class Dialogue : IDataItem
    {
        public ItemType Type => ItemType.Dialogue;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<DialogueLine> Lines { get; set; }

        public IEnumerable<DialogueCondition> Conditions { get; set; }

        public IEnumerable<IItem> WorldStates { get; set; }

        public IEnumerable<IItem> TargetItems { get; set; }

        public IEnumerable<IItem> TargetFactions { get; set; }

        public IEnumerable<IItem> TargetRaces { get; set; }

        public IEnumerable<IItem> SpeakerIsCharacter { get; set; }

        public IEnumerable<IItem> InTownOfFactions { get; set; }

        public IEnumerable<DialogueEvent> Events { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
