// See https://aka.ms/new-console-template for more information
using WeaponStatsConverter;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

var categoriesToIgnore = new string[] { "[[Category:Weapon Types]]", "[[Category:Unique]]" };

using var client = new WikiClient();
var site = new WikiaSite(client, "https://kenshi.fandom.com/api.php");
await site.Initialization;

// set both limits to 1 to allow the bug manifest more easily
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
    var content = page.Content;
    if (IsIgnoredCategory(content) || page.Title.StartsWith("Weapons"))
    {
        Console.WriteLine($"Omitting {page.Title} since it's in an ignored category.");
        continue;
    }

    if ((!content.Contains("=== ''Manufacturer'' ===")
        || !content.Contains("=== ''Homemade'' ===")) && !content.Contains("{{WeaponStats"))
    {
        Console.Error.WriteLine($"{page.Title} does not contain weapon stats table to convert!");
        continue;
    }

    try
    {
        ConvertArticle(page);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"An error occurred when trying to convert {page.Title}: {ex.Message}");
    }
}

void ConvertArticle(WikiPage page)
{
    var reader = new StringReader(page.Content);
    var tableFound = false;
    var line = reader.ReadLine();
    while (line != null)
    {
        if (!tableFound && line != "{| class=\"article-table\" border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"width: 500px; height: 500px;\"")
        {
            line = reader.ReadLine();
            continue;
        }

        var headersValid = ValidateHeaders(reader);
        if (!headersValid)
        {
            Console.Error.WriteLine($"{page.Title} has invalid table headers!");
            break;
        }

        tableFound = true;

        var stats = ReadWeaponStats(reader);
        File.WriteAllText($"{page.Title}.txt", String.Join(Environment.NewLine, stats));
        break; // TODO: Homemade
    }

    if (!tableFound)
    {
        throw new InvalidDataException($"{page.Title} has no valid table!");
    }

    Console.WriteLine($"Saved file {page.Title}.txt");
}

IEnumerable<WeaponStats> ReadWeaponStats(StringReader reader)
{
    var results = new List<WeaponStats>();
    for (var i = 0; i < 15; i++)
    {
        var newRow = reader.ReadLine();
        if (newRow != "|-")
        {
            if (newRow == "|}")
            {
                return results; // plank exception
            }

            throw new InvalidDataException($"Expected '|-', instead got: '{newRow}'");
        }

        var manufacturer = reader.ReadLine().Replace("|", "").Replace("[", "").Replace("]", "").Trim();
        var model = reader.ReadLine().Replace("|", "").Trim();
        var weight = reader.ReadLine().Replace("|", "").Trim();
        var cuttingDamage = reader.ReadLine().Replace("|", "").Trim();
        var bluntDamage = reader.ReadLine().Replace("|", "").Trim();
        var line = reader.ReadLine();
        string attackModifier = null;
        string defenceModifier = null;
        string requiredStrength = null;
        if (line.Contains("Atk"))
        {
            attackModifier = line.Replace("|", "").Trim();
        }
        else if (line.Contains("Def"))
        {
            defenceModifier = line.Replace("|", "").Trim();
        }

        if (!line.Contains("N/A"))
        {
            line = reader.ReadLine();
            if (line.StartsWith("|"))
            {
                requiredStrength = line;
            }
            else if (line.Contains("Atk"))
            {
                attackModifier = line.Replace("|", "").Trim();
            }
            else if (line.Contains("Def"))
            {
                defenceModifier = line.Replace("|", "").Trim();
            }
        }

        if (requiredStrength == null)
        {
            requiredStrength = reader.ReadLine().Replace("|", "").Trim();
        }

        var buyValue = reader.ReadLine().Replace("|", "").Trim();
        var sellValue = reader.ReadLine().Replace("|", "").Trim();

        results.Add(new WeaponStats()
        {
            Manufacturer = manufacturer,
            Grade = model,
            Weight = weight,
            CuttingDamage = cuttingDamage,
            BluntDamage = bluntDamage,
            AttackModifier = attackModifier,
            DefenceModifier = defenceModifier,
            RequiredStrength = requiredStrength,
            BuyValue = buyValue,
            SellValue = sellValue,
        });
    }
    return results;
}

bool ValidateHeaders(StringReader reader)
{
    var headers = new string[]
    {
        "!'''Manufacturer(s)'''",
        "!'''Model'''",
        "!'''Weight (kg)'''",
        "!Cutting Damage",
        "!'''Blunt Damage'''",
        "!'''Attack & Defense Modifiers'''",
        "!'''Required Strength Level'''",
        "!Buy '''Value'''",
        "!Sell Value"
    };

    foreach (var header in headers)
    {
        var line = reader.ReadLine();
        if (!header.Equals(line))
        {
            return false;
        }
    }

    return true;
}

bool IsIgnoredCategory(string content)
{
    return categoriesToIgnore.Any(toIgnore => content.Contains(toIgnore));
}