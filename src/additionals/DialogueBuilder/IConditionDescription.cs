namespace DialogueDumper
{
    public interface IConditionDescription
    {
        public string GetDescription(string speakers, int value, char compareOperator, object? tag);
    }
}
