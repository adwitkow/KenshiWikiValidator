using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialogueAction : ItemBase
    {
        public DialogueAction(Dictionary<string, object> properties, string stringId, string name)
            : base(properties, stringId, name)
        {
        }

        public override ItemType Type => ItemType.DialogAction;
    }
}
