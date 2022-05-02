// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.Characters;
using KenshiWikiValidator.WikiCategories.Locations;
using KenshiWikiValidator.WikiCategories.TownResidents;
using KenshiWikiValidator.WikiCategories.Weapons;
using KenshiWikiValidator.WikiTemplates;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

var zoneDataProvider = new ZoneDataProvider();
await zoneDataProvider.Load();

var itemRepository = new ItemRepository();
var templateParser = new TemplateParser();
var wikiTitles = new WikiTitleCache();
var townResidentValidator = new TownResidentArticleValidator(itemRepository, wikiTitles);
var validators = new List<IArticleValidator>()
{
    new CharactersArticleValidator(itemRepository, wikiTitles),
    townResidentValidator,
    new LocationsArticleValidator(itemRepository, zoneDataProvider, wikiTitles),
    new WeaponArticleValidator(itemRepository, wikiTitles, townResidentValidator),
};

var output = "output";
if (Directory.Exists(output))
{
    Console.WriteLine("Clearing the output directory...");
    Directory.Delete(output, true);
}

Console.WriteLine("Loading items...");
var sw = Stopwatch.StartNew();
itemRepository.Load();
sw.Stop();

Console.WriteLine($"Loaded all items in {sw.Elapsed}");
Console.WriteLine();

Console.WriteLine("Please choose which of the following Wiki categories you wish to validate:");
for (int i = 1; i <= validators.Count; i++)
{
    Console.WriteLine($"[{i}] {validators[i - 1].CategoryName}");
}

Console.WriteLine();

var response = (int)char.GetNumericValue(Console.ReadKey().KeyChar);

if (response < 1 || response > validators.Count)
{
    return 1;
}

using var client = new WikiClient();

var validator = validators[response - 1];

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.White;
if (validator.Dependencies.Any())
{
    Console.WriteLine($"This validator depends on the following categories: {string.Join(", ", validator.Dependencies.Select(dependency => dependency.CategoryName))}");
    foreach (var dependency in validator.Dependencies)
    {
        await RetrieveAndValidate(dependency, client);
    }
}

await RetrieveAndValidate(validator, client);

return 0;

static async Task RetrieveAndValidate(IArticleValidator validator, WikiClient client)
{
    Console.WriteLine("Retrieving the articles for category: " + validator.CategoryName + "...");

    var pages = await RetrieveArticles(client, validator.CategoryName);

    Console.WriteLine("Retrieved. Beginning to validate.");

    Console.ResetColor();

    foreach (var page in pages)
    {
        await page.RefreshAsync(PageQueryOptions.FetchContent);

        ValidateArticle(page, validator);
    }
}

static void ValidateArticle(WikiPage page, IArticleValidator articleValidator)
{
    try
    {
        var result = articleValidator.Validate(page.Title, page.Content);

        var issueGroups = result.Issues.GroupBy(issue => issue);

        foreach (var issueGroup in issueGroups)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(page.Title);
            Console.ResetColor();
            Console.Write($": {issueGroup.Key} ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"({issueGroup.Count()})");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"An exception has been thrown in the article: '{page.Title}'");
        Console.WriteLine(ex);
        Console.ResetColor();
    }
}

static async Task<IEnumerable<WikiPage>> RetrieveArticles(WikiClient client, string category)
{
    var site = new WikiaSite(client, "https://kenshi.fandom.com/api.php");
    await site.Initialization;

    var generator = new CategoryMembersGenerator(site)
    {
        CategoryTitle = $"Category:{category}",
        MemberTypes = CategoryMemberTypes.Page,
        PaginationSize = 20,
    };

    return await generator.EnumPagesAsync().ToListAsync();
}