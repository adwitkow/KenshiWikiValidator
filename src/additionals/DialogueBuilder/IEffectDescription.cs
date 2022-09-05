using KenshiWikiValidator.OcsProxy.DialogueComponents;

namespace DialogueDumper
{
    public interface IEffectDescription
    {
        public string GetDescription(Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, DialogueSpeaker speaker, int? value, IEnumerable<DialogueEvent> dialogueEvents);
    }
}