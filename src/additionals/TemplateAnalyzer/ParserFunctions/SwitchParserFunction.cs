using System.Reflection.Metadata.Ecma335;

namespace TemplateAnalyzer.ParserFunctions
{
    internal class SwitchParserFunction : ParserFunction
    {
        public SwitchParserFunction(string name, IEnumerable<string> arguments)
            : base(name, arguments)
        {
        }

        public string InputArgument => Arguments.First();

        public IEnumerable<string> Cases => Arguments.Skip(1)
            .Where(arg => !IsDefault(arg))
            .Select(arg => arg.Split("=").First().Trim());

        public string? DefaultCase => Arguments.Skip(1).SingleOrDefault(IsDefault);

        private static bool IsDefault(string argument)
        {
            return argument.Contains("#default") || !argument.Contains("=");
        }
    }
}