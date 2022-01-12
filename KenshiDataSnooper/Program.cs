using System.Text.RegularExpressions;
using KenshiDataSnooper;
using Newtonsoft.Json;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

var itemBuilder = new ItemBuilder();
var installationDirectory = string.Empty;
var typesToIgnore = new ItemType[]
{
    ItemType.DialogAction,
    ItemType.Attachment,
    ItemType.FoliageMesh,
    ItemType.DialogueLine,
    ItemType.FoliageLayer,
    ItemType.MapFeatures,
    ItemType.BuildingPart,
    ItemType.AnimalAnimation,
    ItemType.MaterialSpecsClothing,
};

var stringIdRegex = new Regex(@"\d+-\w+\.\w+");
var lookup = new Dictionary<string, DataItem>();

Console.WriteLine("Loading items...");

var items = LoadItems();

var iconsDirectoryInfo = new DirectoryInfo(Path.Combine(installationDirectory, "data", "icons"));

WriteDetails(items, ItemType.Armour);
WriteDetails(items, ItemType.Weapon);
WriteDetails(items, ItemType.Character);

Console.WriteLine("Finished.");

IEnumerable<DataItem> LoadItems()
{
    var installations = OcsDiscoveryService.Default.DiscoverAllInstallations();
    var installation = installations.Values.First();
    var baseMods = installation.Data.Mods;

    installationDirectory = installation.Game;

    var options = new OcsDataContexOptions(
        Name: Guid.NewGuid().ToString(),
        Installation: installation,
        LoadGameFiles: ModLoadType.Base,
        LoadEnabledMods: ModLoadType.Base,
        ThrowIfMissing: false);

    var items = OcsDataContextBuilder.Default.Build(options).Items.Values.ToList();

    lookup = items.ToDictionary(item => item.StringId, item => item);

    return items;
}

void WriteDetails(IEnumerable<DataItem> items, ItemType type)
{
    var itemsOfType = items.Where(item => item.Type == type);

    if (Directory.Exists(type.ToString()))
    {
        Directory.Delete(type.ToString(), true);
    }

    Directory.CreateDirectory(type.ToString());
    foreach (var item in itemsOfType)
    {
        var built = itemBuilder.BuildFrom(item);
        var trimmedName = built.Name.Replace("/", string.Empty).Replace("|", string.Empty).Trim();
        Directory.CreateDirectory(@$"{type}\{trimmedName}");
        File.WriteAllText($@"{type}\{trimmedName}\{trimmedName}-{built.StringId}.json", JsonConvert.SerializeObject(built, Formatting.Indented));
        Console.WriteLine($"Writing {trimmedName}...");

        var references = items.Where(item => item.IsReferencing(item.StringId));
        if (references.Any())
        {
            Directory.CreateDirectory(@$"{type}\{trimmedName}\references");
        }

        foreach (var reference in references)
        {
            var referenceTrimmed = reference.Name.Replace("/", string.Empty).Replace("|", string.Empty).Trim();
            File.WriteAllText($@"{type}\{trimmedName}\references\{referenceTrimmed}-{reference.StringId}.json", JsonConvert.SerializeObject(reference, Formatting.Indented));
        }

        var files = iconsDirectoryInfo.GetFiles($"*{item.StringId}*.*", new EnumerationOptions() { RecurseSubdirectories = true });

        if (files.Any())
        {
            Directory.CreateDirectory(@$"{type}\{trimmedName}\icons");
        }

        foreach (var file in files)
        {
            var idMatches = stringIdRegex.Matches(file.Name);
            var ids = idMatches
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var idItems = ids.Select(id => lookup[id]);
            var names = idItems.Select(idItem => $"{idItem.Name}");
            var joined = string.Join(".", names);
            file.CopyTo(@$"{type}\{trimmedName}\icons\{item.StringId}.{joined}{file.Extension}");
        }
    }
}