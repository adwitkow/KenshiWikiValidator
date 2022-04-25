namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialogueCondition : DialogueAction
    {
        public DialogueCondition(Dictionary<string, object> properties, string stringId, string name) : base(properties, stringId, name)
        {
        }

        public DialogueConditionName ConditionName { get; set; }

        public DialogueSpeaker Who { get; set; }
    }
}
