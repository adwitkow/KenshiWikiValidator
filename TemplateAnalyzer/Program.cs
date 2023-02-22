﻿using System.Text.Json;
using System.Text.RegularExpressions;
using TemplateAnalyzer.ParserFunctions;
using TemplateAnalyzer.Template;
using WikiClientLibrary;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Pages.Parsing;
using WikiClientLibrary.Sites;

const string WikiApiUrl = "https://kenshi.fandom.com/api.php";
var parameterRegex = new Regex(@"{{{(.+?)(\||\|.+)?}}}");
var noIncludeRegex = new Regex(@"<noinclude>.+</noinclude>");
var parserFunctionBeginRegex = new Regex(@"{{#(.+?):");

if (Directory.Exists("Templates"))
{
    Directory.Delete("Templates", true);
}

Directory.CreateDirectory("Templates");

using var client = new WikiClient();
var site = new WikiSite(client, WikiApiUrl);
await site.Initialization;

var generator = new AllPagesGenerator(site)
{
    NamespaceId = BuiltInNamespaces.Template
};

var pages = generator.EnumPagesAsync();

var pageMap = new Dictionary<WikiPage, int>();
await foreach (var page in pages)
{
    var lowercaseTitle = page.Title.ToLower();
    if (lowercaseTitle.EndsWith("/doc") || lowercaseTitle.EndsWith("/sandbox") || lowercaseTitle.EndsWith("/testcases"))
    {
        continue;
    }

    Console.Write($"Mapping {page.Title}...");
    if (page.Title is "Template:Join" or "Template:Reflist")
    {
        Console.WriteLine("Skipped!");
        continue;
    }

    var transGenerator = new TranscludedInGenerator(site)
    {
        TargetTitle = page.Title,
    };
    var transclusions = await transGenerator.EnumPagesAsync().CountAsync();

    Console.Write("Transclusions fetched...");

    await page.RefreshAsync(PageQueryOptions.FetchContent);
    var templateData = ParseTemplate(page.Content);
    var serializedData = JsonSerializer.Serialize(templateData, new JsonSerializerOptions()
    {
        WriteIndented = true,
    });
    var cleanTitle = page.Title.Replace("/", "").Replace("Template:", "");
    File.WriteAllText(Path.Combine("Templates", $"TemplateData-{cleanTitle}.txt"), serializedData);

    pageMap.Add(page, transclusions);
    Console.WriteLine("Done!");
}

var ordered = pageMap.OrderByDescending(pair => pair.Value)
    .ThenBy(pair => pair.Key.Title);
foreach (var pair in ordered)
{
    var page = pair.Key;
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{pair.Value}\t{page.Title}");

    if (!page.Content.Contains("<templatedata>"))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\tTemplateData is missing");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\tTemplateData is present");
    }

    if (!page.Content.Contains("{{Documentation}}"))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("\tDocumentation is missing");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\tDocumentation is present");
    }

    Console.WriteLine();

}

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Finished listing {ordered.Count()} template pages.");

IEnumerable<string> ExtractParameters(string content)
{
    var parameters = new List<string>();
    var matches = parameterRegex.Matches(content);
    foreach (var match in matches.Cast<Match>())
    {
        parameters.Add(match.Groups[1].Value);
    }

    return parameters.Distinct();
}

TemplateData ParseTemplate(string content)
{
    var noIncludeMatches = noIncludeRegex.Matches(content);
    var cleanContent = content;
    foreach (var match in noIncludeMatches.Cast<Match>())
    {
        cleanContent = cleanContent.Replace(match.Value, string.Empty);
    }

    var parserFunctionMatches = parserFunctionBeginRegex.Matches(cleanContent);
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
        var matchingFunctions = switchFunctions.Where(func => parameterRegex.IsMatch(func.InputArgument));
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