namespace TemplateAnalyzer.ParserFunctions
{
    internal class ParserFunction
    {
        public ParserFunction(string name, IEnumerable<string> arguments)
        {
            this.Name = name;
            this.Arguments = arguments;
        }

        public string Name { get; init; }

        public IEnumerable<string> Arguments { get; init; }

        public static ParserFunction FromString(string name, string content)
        {
            var previousArgument = name.Length + 4;
            var arguments = new List<string>();
            for (int i = 2; i < content.Length; i++)
            {
                var character = content[i];

                if (character == '|')
                {
                    var argument = content[previousArgument..i].Trim();
                    arguments.Add(argument);
                    previousArgument = i + 1;
                    continue;
                }

                if (character == '[')
                {
                    var j = i;
                    var count = 0;
                    while (j < content.Length && content[j] == '[')
                    {
                        j++;
                        count++;
                    }

                    i = FindFunctionBounds(i, content, count, '[', ']');
                    continue;
                }

                if (character == '{')
                {
                    var j = i;
                    var count = 0;
                    while (j < content.Length && content[j] == '{')
                    {
                        j++;
                        count++;
                    }

                    i = FindFunctionBounds(i, content, count, '{', '}');
                }
            }

            var lastArgument = content[previousArgument..^2].Trim();
            arguments.Add(lastArgument);

            return name switch
            {
                "if" => new IfParserFunction(name, arguments),
                "ifeq" => new IfEqParserFunction(name, arguments),
                "switch" => new SwitchParserFunction(name, arguments),
                _ => new ParserFunction(name, arguments),
            };
        }

        public static int FindFunctionBounds(int startingIndex, string content, int functionMarkerCount, char openFunction, char closeFunction)
        {
            for (int i = startingIndex + functionMarkerCount + 1; i < content.Length; i++)
            {
                var character = content[i];
                if (character == closeFunction)
                {
                    var j = i;
                    var count = 0;
                    while (j < content.Length && content[j] == closeFunction)
                    {
                        j++;
                        count++;

                        if (count == functionMarkerCount)
                        {
                            return i + functionMarkerCount - 1;
                        }
                    }
                }

                if (character == '[')
                {
                    var j = i;
                    var count = 0;
                    while (j < content.Length && content[j] == '[')
                    {
                        j++;
                        count++;
                    }

                    i = FindFunctionBounds(i, content, count, '[', ']');
                    continue;
                }

                if (character == '{')
                {
                    var j = i;
                    var count = 0;
                    while (j < content.Length && content[j] == '{')
                    {
                        j++;
                        count++;
                    }

                    i = FindFunctionBounds(i, content, count, '{', '}');
                }
            }

            return -1;
        }

        public override string ToString()
        {
            return $"{{{{#{Name}: {string.Join(" | ", Arguments)}}}}}";
        }
    }
}
