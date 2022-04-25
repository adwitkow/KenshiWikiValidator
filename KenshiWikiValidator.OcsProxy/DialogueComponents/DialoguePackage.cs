using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialoguePackage : ItemBase
    {
        public DialoguePackage(string stringId, string name) : this(new Dictionary<string, object>(), stringId, name)
        {
        }

        public DialoguePackage(IDictionary<string, object> properties, string stringId, string name)
            : base(properties, stringId, name)
        {
            this.Dialogues = Enumerable.Empty<Dialogue>();
        }

        public override ItemType Type => ItemType.DialoguePackage;

        public IEnumerable<Dialogue> Dialogues { get; set; }
    }
}
