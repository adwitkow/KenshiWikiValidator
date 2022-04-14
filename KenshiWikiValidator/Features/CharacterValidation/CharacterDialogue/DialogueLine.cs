using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialogueLine : IDataItem
    {
        public ItemType Type => ItemType.DialogueLine;

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<DialogueEffect> Effects { get; set; }

        public IEnumerable<Dialogue> UnlockedDialogues { get; set; }

        public IEnumerable<DialogueCondition> Conditions { get; set; }

        public IEnumerable<DialogueLine> Lines { get; set; }

        public Dictionary<string, int> RelationChanges { get; set; }

        public IEnumerable<IItem> TargetedFactions { get; set; }

        public IEnumerable<IItem> TargetedRaces { get; set; }

        public IEnumerable<IItem> CharactersCarriedByTarget { get; set; }

        public IEnumerable<IItem> WorldStates { get; set; }

        public IEnumerable<IItem> InTownOfFactions { get; set; }

        public IEnumerable<Dialogue> CrowdTriggers { get; set; }

        public IEnumerable<IItem> SpeakerRaces { get; set; }

        public IEnumerable<IItem> SpeakerFactions { get; set; }

        public IEnumerable<IItem> TargetItems { get; set; }

        public IEnumerable<IItem> SpeakerSubraces { get; set; }

        public IEnumerable<IItem> SpeakerIsCharacter { get; set; }

        public IEnumerable<IItem> GivenItem { get; set; }

        public IEnumerable<IItem> TriggeredCampaigns { get; set; }

        public DialogueSpeaker Speaker { get; set; }
    }
}
