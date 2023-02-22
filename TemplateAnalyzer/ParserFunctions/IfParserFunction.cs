namespace TemplateAnalyzer.ParserFunctions
{
    internal class IfParserFunction : ParserFunction
    {
        public IfParserFunction(string name, IEnumerable<string> arguments)
            : base(name, arguments)
        {
            if (arguments.Count() is < 2 or > 3)
            {
                throw new ArgumentException("'If' parser function requires 2 or 3 arguments.");
            }
        }

        public string Condition => Arguments.First();

        public string ValueIfTrue => Arguments.Skip(1).First();

        public string ValueIfFalse => Arguments.Count() == 3 ? Arguments.Last() : string.Empty;
    }
}
