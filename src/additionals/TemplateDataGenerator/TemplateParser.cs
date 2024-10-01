using System.Text.RegularExpressions;
using TemplateDataGenerator.ParserFunctions;
using TemplateDataGenerator.Template;

namespace TemplateDataGenerator
{
    public class TemplateParser
    {
        private static readonly Regex ParameterRegex = new Regex(@"{{{(.+?)(\||\|.+)?}}}");
        private static readonly Regex NoIncludeRegex = new Regex(@"<noinclude>.+</noinclude>");
        private static readonly Regex ParserFunctionBeginRegex = new Regex(@"{{#(.+?):");

        public TemplateData ParseTemplate(string content)
        {
            var noIncludeMatches = NoIncludeRegex.Matches(content);
            var cleanContent = content;
            foreach (var match in noIncludeMatches.Cast<Match>())
            {
                cleanContent = cleanContent.Replace(match.Value, string.Empty);
            }

            var parserFunctionMatches = ParserFunctionBeginRegex.Matches(cleanContent);
            var parserFunctions = new List<ParserFunction>();
            var bounds = 0;
            foreach (var match in parserFunctionMatches.Cast<Match>())
            {
                var name = match.Groups[1].Value;

                if (bounds > match.Index)
                {
                    continue;
                }

                bounds = ParserFunction.FindFunctionBounds(match.Index, cleanContent, 2, '{', '}');

                var result = cleanContent.Substring(match.Index, bounds - match.Index + 1);
                parserFunctions.Add(ParserFunction.FromString(name.ToLower(), result));
            }

            var ifFunctions = parserFunctions.OfType<IfParserFunction>().ToList();
            var ifEqFunctions = parserFunctions.OfType<IfEqParserFunction>().ToList();
            var switchFunctions = parserFunctions.OfType<SwitchParserFunction>().ToList();
            var remainingFunctions = parserFunctions.Except(ifFunctions)
                .Except(switchFunctions)
                .Except(ifEqFunctions)
                .ToList();

            var parameters = ExtractParameters(cleanContent).ToDictionary(param => param, param =>
            {
                var matchingFunctions = switchFunctions.Where(func => ParameterRegex.IsMatch(func.InputArgument));
                SwitchParserFunction? switchFunction = null;
                if (matchingFunctions.Count() == 1)
                {
                    switchFunction = matchingFunctions.First();
                }
                else if (matchingFunctions.Count() > 1)
                {
                    foreach (var function1 in matchingFunctions)
                    {
                        var finished = false;
                        foreach (var function2 in matchingFunctions)
                        {
                            if (function1 == function2)
                            {
                                continue;
                            }

                            var areEqual = function1.Cases.All(function2.Cases.Contains) && function2.Cases.All(function1.Cases.Contains);
                            if (!areEqual)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"'{param}' parameter was matched by more than 1 unique switch function");
                                Console.ResetColor();
                                finished = true;
                                break;
                            }
                        }
                        if (finished)
                        {
                            break;
                        }
                    }
                }

                var suggestedValues = switchFunction != null ? switchFunction.Cases : Enumerable.Empty<string>();
                return new TemplateParameter()
                {
                    Label = "TODO",
                    Aliases = new List<string>(),
                    AutoValue = "TODO",
                    Default = "TODO",
                    Deprecated = "TODO",
                    Description = "TODO",
                    Example = "TODO",
                    Inherits = "TODO",
                    Required = false,
                    Suggested = false,
                    SuggestedValues = suggestedValues,
                    Type = "TODO",
                };
            });

            return new TemplateData()
            {
                Description = "TODO",
                Format = "TODO",
                ParameterOrder = new string[] { },
                Parameters = parameters,
                ParameterSets = new ParameterSet[] { },
            };
        }

        private IEnumerable<string> ExtractParameters(string content)
        {
            var parameters = new List<string>();
            var matches = ParameterRegex.Matches(content);
            foreach (var match in matches.Cast<Match>())
            {
                parameters.Add(match.Groups[1].Value);
            }

            return parameters.Distinct();
        }
    }
}
