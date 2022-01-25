using System.Diagnostics;
using System.Text.RegularExpressions;
using KenshiDataSnooper;
using KenshiDataSnooper.Models;
using Newtonsoft.Json;
using OpenConstructionSet.Models;

var repository = new ItemRepository();
var referenceResolver = new ReferenceResolver();
var stringIdRegex = new Regex(@"\d+-\w+\.\w+");

Console.WriteLine("Loading items...");
var sw = Stopwatch.StartNew();
repository.Load();
sw.Stop();

Console.WriteLine($"Loaded all items in {sw.Elapsed}");

var iconsDirectoryInfo = new DirectoryInfo(Path.Combine(repository.GameDirectory!, "data", "icons"));

var items = repository.GetItems();

var colourSchemeItems = new List<string>();

WriteDetails(items, ItemType.Armour);
WriteDetails(items, ItemType.Weapon);

Console.WriteLine("Finished.");
Console.WriteLine();
Console.WriteLine($"Items with colour schemes: {string.Join(", ", colourSchemeItems)}");

void WriteDetails(IEnumerable<IItem> items, ItemType type)
{
    var itemsOfType = items.Where(item => item.Type == type);

    if (Directory.Exists(type.ToString()))
    {
        Directory.Delete(type.ToString(), true);
    }

    Directory.CreateDirectory(type.ToString());
    foreach (var item in itemsOfType)
    {
        var trimmedName = item.Name.Replace("/", string.Empty).Replace("|", string.Empty).Trim();
        var directory = Path.Combine(type.ToString(), trimmedName);
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, $"{trimmedName}-{item.StringId}.json"), JsonConvert.SerializeObject(item, Formatting.Indented));
        Console.WriteLine($"Writing {trimmedName}...");

        var files = iconsDirectoryInfo.GetFiles($"*{item.StringId}*.*", new EnumerationOptions() { RecurseSubdirectories = true });

        if (item.Properties!.ContainsKey("icon"))
        {
            var iconFileValue = (FileValue)item.Properties["icon"];
            if (!string.IsNullOrEmpty(iconFileValue.Path))
            {
                var path = Path.GetFullPath(repository.GameDirectory! + iconFileValue.Path);
                var iconInfo = new FileInfo(path);
                files = files.Concat(new[] { iconInfo }).ToArray();
            }
        }

        if (files.Any())
        {
            Directory.CreateDirectory(Path.Combine(directory, "icons"));
        }

        foreach (var file in files)
        {
            var idMatches = stringIdRegex.Matches(file.Name);
            var ids = idMatches
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            var idItems = ids.Select(id => repository.GetDataItemByStringId(id));

            var material = idItems.FirstOrDefault(i => i.Type == ItemType.MaterialSpecsWeapon
                || item.Type == ItemType.MaterialSpecsClothing)?.Name.Trim();
            var name = item.Name.Trim();
            var colour = idItems.FirstOrDefault(i => i.Type == ItemType.ColorData)?.Name.Trim();

            var names = new[] { material, name, colour };

            if (colour != null && !colourSchemeItems.Contains(name))
            {
                colourSchemeItems.Add(name);
            }

            var joined = string.Join(" ", names).Trim();
            var targetPath = Path.Combine(directory, "icons", $"{joined}{file.Extension}");

            var index = 1;
            while (File.Exists(targetPath))
            {
                targetPath = Path.Combine(directory, "icons", $"{joined} ({index}){file.Extension}");
                index++;
            }

            file.CopyTo(targetPath);
        }
    }
}