// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using WeaponStatsConverter;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Wikia.Sites;

const string WikiApiUrl = "https://kenshi.fandom.com/api.php";

var categoriesToIgnore = new string[] { "[[Category:Weapon Types]]", "[[Category:Unique]]" };

var classRegex = new Regex(@"\| ?class ?= ?(?<class>\w+)");
var bloodLossRegex = new Regex(@"\| ?blood loss ?= ?(?<bloodloss>(\d|,|\.)+)");
var armourPenetrationRegex = new Regex(@"\| ?armour penetration ?= ?(?<armourpen>(-|\+)(\d|,|\.)+)");
var indoorsRegex = new Regex(@"\| ?indoors ?= ?(?<indoors>(-|\+)(\d|,|\.)+)");
var robotsRegex = new Regex(@"\| ?damage_robots ?= ?(?<robots>(-|\+)(\d|,|\.)+)");
var humansRegex = new Regex(@"\| ?damage_humans ?= ?(?<humans>(-|\+)(\d|,|\.)+)");
var animalsRegex = new Regex(@"\| ?damage_animals ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var spiderRegex = new Regex(@"\| ?damage_spider ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var smallSpiderRegex = new Regex(@"\| ?damage_small spider ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var bonedogRegex = new Regex(@"\| ?damage_bonedog ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var skimmerRegex = new Regex(@"\| ?damage_skimmer ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var beakThingRegex = new Regex(@"\| ?damage_beak thing ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var gorilloRegex = new Regex(@"\| ?damage_gorillo ?= ?(?<animals>(-|\+)(\d|,|\.)+)");
var leviathanRegex = new Regex(@"\| ?damage_leviathan ?= ?(?<animals>(-|\+)(\d|,|\.)+)");

if (Directory.Exists("Weapons"))
{
    Directory.Delete("Weapons", true);
}

Directory.CreateDirectory("Weapons");

using var client = new WikiClient();
var site = new WikiaSite(client, WikiApiUrl);
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

    if (content.Contains("{{WeaponStats"))
    {
        Console.WriteLine($"{page.Title} already contains WeaponStats.");
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

    var manufacturerStats = ConvertTable(reader, page, false);
    var homemadeStats = ConvertTable(reader, page, true);

    File.WriteAllText($"Weapons/{page.Title}.txt", String.Join(Environment.NewLine, manufacturerStats));
    File.WriteAllText($"Weapons/{page.Title}_Homemade.txt", String.Join(Environment.NewLine, homemadeStats));

    Console.WriteLine($"Saved WeaponStats for {page.Title}");
}

IEnumerable<WeaponStats> ConvertTable(StringReader reader, WikiPage page, bool homemade)
{
    var line = reader.ReadLine();

    while (line != null)
    {
        if (line != "{| class=\"article-table\" border=\"0\" cellpadding=\"1\" cellspacing=\"1\" style=\"width: 500px; height: 500px;\"")
        {
            line = reader.ReadLine();
            continue;
        }

        var headersValid = ValidateHeaders(reader);
        if (!headersValid)
        {
            throw new InvalidDataException($"{page.Title} has invalid table headers!");
        }

        var weaponClass = CaptureRegex(classRegex, page.Content, "class");
        var bloodLoss = CaptureRegex(bloodLossRegex, page.Content, "bloodloss");
        var armourPenetration = CaptureRegex(armourPenetrationRegex, page.Content, "armourpen");
        var indoors = CaptureRegex(indoorsRegex, page.Content, "indoors");
        var humans = CaptureRegex(humansRegex, page.Content, "humans");
        var robots = CaptureRegex(robotsRegex, page.Content, "robots");
        var animals = CaptureRegex(animalsRegex,page.Content,"animals");
        var spider = CaptureRegex(spiderRegex, page.Content,"animals");
        var smallSpider = CaptureRegex(smallSpiderRegex, page.Content,"animals");
        var bonedog = CaptureRegex(bonedogRegex, page.Content,"animals");
        var skimmer = CaptureRegex(skimmerRegex, page.Content,"animals");
        var beakThing = CaptureRegex(beakThingRegex, page.Content,"animals");
        var gorillo = CaptureRegex(gorilloRegex, page.Content,"animals");
        var leviathan = CaptureRegex(leviathanRegex, page.Content,"animals");

        var stats = ReadWeaponStats(reader);

        foreach (var stat in stats)
        {
            stat.Class = weaponClass;
            stat.BloodLoss = bloodLoss;
            stat.ArmourPenetration = armourPenetration;
            stat.IndoorsModifier = indoors;
            stat.DamageVersusHumans = humans;
            stat.DamageVersusRobots = robots;
            stat.DamageVersusAnimals = animals;
            stat.DamageVersusSpider= spider;
            stat.DamageVersusSmallSpider = smallSpider;
            stat.DamageVersusBonedog = bonedog;
            stat.DamageVersusSkimmer = skimmer;
            stat.DamageVersusBeakThing = beakThing;
            stat.DamageVersusGorillo = gorillo;
            stat.DamageVersusLeviathan = leviathan;

            if (homemade)
            {
                stat.Manufacturer = "Homemade";
            }
        }

        return stats;
    }

    throw new InvalidDataException($"{page.Title} has no valid table!");
}

string CaptureRegex(Regex regex, string content, string groupName)
{
    var match = regex.Match(content);
    return match.Groups[groupName].Value;
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

        var manufacturer = reader.ReadLine()?.Replace("|", "").Replace("[", "").Replace("]", "").Trim();
        var model = reader.ReadLine()?.Replace("|", "").Trim();
        var weight = reader.ReadLine()?.Replace("|", "").Replace("kg", "").Trim();
        var cuttingDamage = reader.ReadLine()?.Replace("|", "").Trim();
        var bluntDamage = reader.ReadLine()?.Replace("|", "").Trim();
        var line = reader.ReadLine();
        string? attackModifier = null;
        string? defenceModifier = null;
        string? requiredStrength = null;

        if (line is null)
        {
            throw new InvalidDataException("Article content has ended abruptly during table reading.");
        }

        if (line.Contains("Atk"))
        {
            attackModifier = line.Replace("|", "").Replace("Atk", "").Replace("<nowiki>", "").Replace("</nowiki>", "").Trim();
        }
        else if (line.Contains("Def"))
        {
            defenceModifier = line.Replace("|", "").Replace("Def", "").Replace("<nowiki>", "").Replace("</nowiki>", "").Trim();
        }

        if (!line.Contains("N/A"))
        {
            line = reader.ReadLine();

            if (line is null)
            {
                throw new InvalidDataException("Article content has ended abruptly during table reading.");
            }

            if (line.StartsWith("|"))
            {
                requiredStrength = line;
            }
            else if (line.Contains("Atk"))
            {
                attackModifier = line.Replace("|", "").Replace("Atk", "").Replace("<nowiki>","").Replace("</nowiki>", "").Trim();
            }
            else if (line.Contains("Def"))
            {
                defenceModifier = line.Replace("|", "").Replace("Def", "").Replace("<nowiki>", "").Replace("</nowiki>", "").Trim();
            }
        }

        if (requiredStrength == null)
        {
            line = reader.ReadLine();
        }

        requiredStrength = line?.Replace("|", "").Replace("Str", "").Replace("<nowiki>", "").Replace("</nowiki>", "").Trim();

        var buyValue = reader.ReadLine()?.Replace("|", "").Trim();
        var sellValue = reader.ReadLine()?.Replace("|", "").Trim();

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
        "!Manufacturer(s)",
        "!Model",
        "!Weight (kg)",
        "!Cutting Damage",
        "!Blunt Damage",
        "!Attack & Defense Modifiers",
        "!Required Strength Level",
        "!Buy Value",
        "!Sell Value"
    };

    foreach (var header in headers)
    {
        var line = reader.ReadLine();

        if (line is null)
        {
            throw new InvalidDataException("Article content has ended abruptly during header validation.");
        }

        if (!header.Equals(line.Replace("'''", "")))
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