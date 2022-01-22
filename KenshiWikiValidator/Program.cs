using KenshiWikiValidator;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

var validatorMap = new Dictionary<string, IArticleValidator>();
if (Directory.Exists("Weapons"))
{
    Directory.Delete("Weapons", true);
}

Directory.CreateDirectory("Weapons");

using var client = new WikiClient();
var site = new WikiaSite(client, "https://kenshi.fandom.com/api.php");
await site.Initialization;

var generator = new CategoryMembersGenerator(site)
{
    CategoryTitle = "Category:Weapons",
    MemberTypes = CategoryMemberTypes.Page,
    PaginationSize = 20,
};

var pages = await generator.EnumPagesAsync().Skip(12).ToListAsync();
foreach (var page in pages)
{
    await Task.Delay(300);
    await page.RefreshAsync(PageQueryOptions.FetchContent);
}