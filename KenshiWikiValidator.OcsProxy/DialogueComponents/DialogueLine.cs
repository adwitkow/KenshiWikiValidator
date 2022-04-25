using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialogueLine : ItemBase
    {
        public DialogueLine(Dictionary<string, object> properties, string stringId, string name) : base(properties, stringId, name)
        {
            this.Lines = Enumerable.Empty<DialogueLine>();
            this.Conditions = Enumerable.Empty<DialogueCondition>();
            this.Effects = Enumerable.Empty<DialogueEffect>();
            this.UnlockedDialogues = Enumerable.Empty<Dialogue>();
            this.WorldStates = Enumerable.Empty<DataItem>();
            this.TargetItems = Enumerable.Empty<DataItem>();
            this.SpeakerIsCharacter = Enumerable.Empty<DataItem>();
            this.InTownOfFactions = Enumerable.Empty<DataItem>();
            this.CharactersCarriedByTarget = Enumerable.Empty<DataItem>();
            this.SpeakerRaces = Enumerable.Empty<DataItem>();
            this.SpeakerSubraces = Enumerable.Empty<DataItem>();
            this.TriggeredCampaigns = Enumerable.Empty<DataItem>();
            this.RelationChanges = new Dictionary<string, int>();
            this.TargetedFactions = Enumerable.Empty<DataItem>();
            this.TargetedRaces = Enumerable.Empty<DataItem>();
            this.CrowdTriggers = Enumerable.Empty<Dialogue>();
            this.SpeakerFactions = Enumerable.Empty<DataItem>();
            this.GivenItem = Enumerable.Empty<DataItem>();
        }

        public override ItemType Type => ItemType.DialogueLine;

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
