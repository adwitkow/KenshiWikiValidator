using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class DialoguePackage : ItemBase
    {
        public DialoguePackage(string stringId, string name)
            : base(stringId, name)
        {
            this.Dialogs = Enumerable.Empty<ItemReference<Dialogue>>();
            this.Inheritsfrom = Enumerable.Empty<ItemReference<DialoguePackage>>();
        }

        public override ItemType Type => ItemType.DialoguePackage;

        [Reference("dialogs")]
        public IEnumerable<ItemReference<Dialogue>> Dialogs { get; set; }

        [Reference("inheritsFrom")]
        public IEnumerable<ItemReference<DialoguePackage>> Inheritsfrom { get; set; }

    }
}