using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class Dialogue : ItemBase
    {
        public Dialogue(Dictionary<string, object> properties, string stringId, string name)
            : base(properties, stringId, name)
        {
            this.Lines = Enumerable.Empty<DialogueLine>();
            this.Conditions = Enumerable.Empty<DialogueCondition>();
            this.WorldStates = Enumerable.Empty<DataItem>();
            this.TargetItems = Enumerable.Empty<DataItem>();
            this.TargetFactions = Enumerable.Empty<DataItem>();
            this.TargetRaces = Enumerable.Empty<DataItem>();
            this.SpeakerIsCharacter = Enumerable.Empty<DataItem>();
            this.InTownOfFactions = Enumerable.Empty<DataItem>();
            this.Events = Enumerable.Empty<DialogueEvent>();
        }

        public override ItemType Type => ItemType.Dialogue;

        public IEnumerable<DialogueLine> Lines { get; set; }

        public IEnumerable<DialogueCondition> Conditions { get; set; }

        public IEnumerable<DataItem> WorldStates { get; set; }

        public IEnumerable<DataItem> TargetItems { get; set; }

        public IEnumerable<DataItem> TargetFactions { get; set; }

        public IEnumerable<DataItem> TargetRaces { get; set; }

        public IEnumerable<DataItem> SpeakerIsCharacter { get; set; }

        public IEnumerable<DataItem> InTownOfFactions { get; set; }

        public IEnumerable<DialogueEvent> Events { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public IEnumerable<DialogueLine> GetAllLines()
        {
            var stack = new Stack<DialogueLine>(this.Lines);
            var results = new List<DialogueLine>();
            while (stack.Any())
            {
                var next = stack.Pop();

                if (results.Contains(next))
                {
                    continue;
                }

                results.Add(next);

                foreach (var child in next.Lines)
                {
                    stack.Push(child);
                }
            }

            return results;
        }
    }
}
