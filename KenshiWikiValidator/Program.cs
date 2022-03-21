using System.Diagnostics;
using KenshiWikiValidator.Features.ArticleValidation;
using KenshiWikiValidator.Features.ArticleValidation.Locations;
using KenshiWikiValidator.Features.ArticleValidation.TownResident;
using KenshiWikiValidator.Features.ArticleValidation.Weapons;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

var itemRepository = new ItemRepository();
var templateParser = new TemplateParser();
var wikiTitles = new WikiTitleCache();
var validators = new List<IArticleValidator>()
{
    new TownResidentArticleValidator(itemRepository, wikiTitles),
    new LocationsArticleValidator(itemRepository, wikiTitles),
    new WeaponArticleValidator(itemRepository, wikiTitles),
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

foreach (var articleValidator in validators)
{
    using var client = new WikiClient();
    var pages = await RetrieveArticles(client, articleValidator.CategoryName);

    Console.WriteLine();
    Console.WriteLine("Validating category: " + articleValidator.CategoryName);

    foreach (var page in pages)
    {
        await Task.Delay(1000);
        await page.RefreshAsync(PageQueryOptions.FetchContent);

        ValidateArticle(page, articleValidator);
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