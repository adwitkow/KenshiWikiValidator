namespace TemplateDataGenerator.ParserFunctions
{
    internal class IfEqParserFunction : ParserFunction
    {
        public IfEqParserFunction(string name, IEnumerable<string> arguments)
            : base(name, arguments)
        {
            if (arguments.Count() is < 3 or > 4)
            {
                throw new ArgumentException("'Ifeq' parser function requires 3 or 4 arguments.");
            }
        }

        public string Input1 => Arguments.First();

        public string Input2 => Arguments.Skip(1).First();

        public string ValueIfEqual => Arguments.Skip(2).First();

        public string ValueIfDifferent => Arguments.Last();
    }
}