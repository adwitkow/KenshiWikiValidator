using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialogueLine : IItem
    {
        public ItemType Type => ItemType.DialogueLine;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }

        public IEnumerable<DialogueEffect> Effects { get; set; }

        public IEnumerable<Dialogue> UnlockedDialogues { get; set; }

        public IEnumerable<DialogueCondition> Conditions { get; set; }

        public IEnumerable<DialogueLine> Lines { get; set; }

        public Dictionary<string, int> RelationChanges { get; set; }

        public IEnumerable<DataItem> TargetedFactions { get; set; }

        public IEnumerable<DataItem> TargetedRaces { get; set; }

        public IEnumerable<DataItem> CharactersCarriedByTarget { get; set; }

        public IEnumerable<DataItem> WorldStates { get; set; }

        public IEnumerable<DataItem> InTownOfFactions { get; set; }

        public IEnumerable<Dialogue> CrowdTriggers { get; set; }

        public IEnumerable<DataItem> SpeakerRaces { get; set; }

        public IEnumerable<DataItem> SpeakerFactions { get; set; }

        public IEnumerable<DataItem> TargetItems { get; set; }

        public IEnumerable<DataItem> SpeakerSubraces { get; set; }

        public IEnumerable<DataItem> SpeakerIsCharacter { get; set; }

        public IEnumerable<DataItem> GivenItem { get; set; }

        public IEnumerable<DataItem> TriggeredCampaigns { get; set; }

        public DialogueSpeaker Speaker { get; set; }
    }
}
