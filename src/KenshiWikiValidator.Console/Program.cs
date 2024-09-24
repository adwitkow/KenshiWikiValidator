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
using KenshiWikiValidator.Armours;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.Characters;
using KenshiWikiValidator.Console;
using KenshiWikiValidator.Locations;
using KenshiWikiValidator.MapItems;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.TownResidents;
using KenshiWikiValidator.Weapons;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

const string WikiApiUrl = "https://kenshi.fandom.com/api.php";

var output = "output";
if (Directory.Exists(output))
{
    Console.WriteLine("Clearing the output directory...");
    Directory.Delete(output, true);
}

var zoneDataProvider = new ZoneDataProvider();
await zoneDataProvider.Load();

var contextProvider = new ContextProvider();
var context = contextProvider.GetDataMiningContext();
var itemRepository = new ItemRepository(context);

Console.WriteLine("Loading items...");
var sw = Stopwatch.StartNew();
itemRepository.Load();
sw.Stop();

Console.WriteLine($"Loaded all items in {sw.Elapsed}");
Console.WriteLine();

var wikiTitles = new WikiTitleCache();
var townResidentValidator = new TownResidentArticleValidator(itemRepository, wikiTitles);
var validators = new List<IArticleValidator>()
{
    new CharactersArticleValidator(itemRepository, wikiTitles),
    townResidentValidator,
    new LocationsArticleValidator(itemRepository, zoneDataProvider, wikiTitles),
    new WeaponArticleValidator(itemRepository, wikiTitles, townResidentValidator),
    new MapItemArticleValidator(itemRepository, wikiTitles, zoneDataProvider),
    new ArmourArticleValidator(itemRepository, wikiTitles),
};

Console.WriteLine("Please choose which of the following Wiki categories you wish to validate:");
for (int i = 1; i <= validators.Count; i++)
{
    Console.WriteLine($"[{i}] {validators[i - 1].CategoryName}");
}

Console.WriteLine();

var response = (int)char.GetNumericValue(Console.ReadKey().KeyChar);
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.White;

if (response < 1 || response > validators.Count)
{
    return 1;
}

using var client = new WikiClient();
var site = new WikiaSite(client, WikiApiUrl);
await site.Initialization;

var validator = validators[response - 1];

var page = new WikiPage(site, "Empire Mercenary");
await page.RefreshAsync(PageQueryOptions.FetchContent);
await CachePage(validator, page);
ValidateArticle(page, validator);

if (validator.Dependencies.Any())
{
    Console.WriteLine($"This validator depends on the following categories: {string.Join(", ", validator.Dependencies.Select(dependency => dependency.CategoryName))}");
    foreach (var dependency in validator.Dependencies)
    {
        await RetrieveAndValidate(dependency, site);
    }
}

await RetrieveAndValidate(validator, site);

validator.AfterValidations();

return 0;

static async Task RetrieveAndValidate(IArticleValidator validator, WikiaSite site)
{
    var exceptions = 0;

    Console.WriteLine("Retrieving the articles for category: " + validator.CategoryName + "...");

    var pages = await RetrieveArticles(site, validator.CategoryName);

    Console.WriteLine($"Retrieved {pages.Count} articles. Proceeding to cache the metadata.");

    Console.ResetColor();

    validator.PopulateStringIds();

    using (var progress = new ProgressBar())
    {
        for (var i = 0; i < pages.Count; i++)
        {
            progress.Report((double)i / pages.Count);
            var page = pages[i];

            await CachePage(validator, page);
        }
    }

    Console.WriteLine("Done. Beginning to validate the articles.");

    foreach (var page in pages)
    {
        exceptions += Convert.ToInt32(ValidateArticle(page, validator));
    }

    if (exceptions > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    Console.WriteLine($"Exceptions: {exceptions}");
    Console.ResetColor();
}

static bool ValidateArticle(WikiPage page, IArticleValidator articleValidator)
{
    if (!articleValidator.ShouldValidate(page.Title, page.Content))
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{page.Title}: validation was skipped...");
        Console.ResetColor();

        return false;
    }

    bool failure;
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

        failure = false;
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"An exception has been thrown in the article: '{page.Title}'");
        Console.WriteLine(ex);
        Console.ResetColor();

        failure = true;
    }

    return failure;
}

static async Task<IList<WikiPage>> RetrieveArticles(WikiaSite site, string category)
{
    var generator = new CategoryMembersGenerator(site)
    {
        CategoryTitle = $"Category:{category}",
        MemberTypes = CategoryMemberTypes.Page,
        PaginationSize = 20,
    };

    return await generator.EnumPagesAsync().ToListAsync();
}

static async Task CachePage(IArticleValidator validator, WikiPage page)
{
    await page.RefreshAsync(PageQueryOptions.FetchContent);

    try
    {
        validator.CachePageData(page.Title, page.Content);
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"An exception has been thrown in the article: '{page.Title}'");
        Console.WriteLine(ex);
        Console.ResetColor();
    }
}