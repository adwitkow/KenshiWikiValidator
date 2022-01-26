using KenshiWikiValidator;
using KenshiWikiValidator.Features.ArticleValidation.Validators;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;
using System.Diagnostics;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

var itemRepository = new ItemRepository();
var templateParser = new TemplateParser();
var validators = new List<IArticleValidator>()
{
    new WeaponArticleValidator(templateParser, itemRepository),
};

Console.WriteLine("Loading items...");
var sw = Stopwatch.StartNew();
itemRepository.Load();
sw.Stop();

Console.WriteLine($"Loaded all items in {sw.Elapsed}");

foreach (var articleValidator in validators)
{
    await ValidateArticle(articleValidator);
}

async Task ValidateArticle(IArticleValidator articleValidator)
{
    var category = articleValidator.CategoryName;
    if (Directory.Exists(category))
    {
        Directory.Delete(category, true);
    }

    Directory.CreateDirectory(category);

    using var client = new WikiClient();
    var site = new WikiaSite(client, "https://kenshi.fandom.com/api.php");
    await site.Initialization;

    var generator = new CategoryMembersGenerator(site)
    {
        CategoryTitle = $"Category:{category}",
        MemberTypes = CategoryMemberTypes.Page,
        PaginationSize = 20,
    };

    var pages = await generator.EnumPagesAsync().ToListAsync();

    foreach (var page in pages)
    {
        await Task.Delay(1000);
        await page.RefreshAsync(PageQueryOptions.FetchContent);

        var result = articleValidator.Validate(page.Title, page.Content);

        var issueGroups = result.Issues.GroupBy(issue => issue);

        foreach (var issueGroup in issueGroups)
        {
            Console.WriteLine($"{page.Title}: {issueGroup.Key} ({issueGroup.Count()})");
        }
    }
}