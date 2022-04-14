namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialogueCondition : DialogueAction
    {
        public DialogueConditionName ConditionName { get; set; }

        public DialogueSpeaker Who { get; set; }
    }
}
