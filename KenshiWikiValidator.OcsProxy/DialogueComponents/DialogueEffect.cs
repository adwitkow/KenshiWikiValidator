namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public class DialogueEffect : DialogueAction
    {
        public DialogueEffect(Dictionary<string, object> properties, string stringId, string name, DialogueEffectName effectName) : base(properties, stringId, name)
        {
            this.EffectName = effectName;
        }

        public DialogueEffectName EffectName { get; }
    }
}
