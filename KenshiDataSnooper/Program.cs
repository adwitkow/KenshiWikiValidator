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
        var trimmedName = item.Name!.Replace("/", string.Empty).Replace("|", string.Empty).Trim();
        var directory = Path.Combine(type.ToString(), trimmedName);
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, $"{trimmedName}-{item.StringId}.json"), JsonConvert.SerializeObject(item, Formatting.Indented));
        Console.WriteLine($"Writing {trimmedName}...");

        if (item is IResearchable researchable)
        {
            string result = string.Empty;
            var color = item.Type switch
            {
                ItemType.Crossbow => "yellow",
                ItemType.Armour => "green",
                _ => "blue",
            };

            if (researchable.UnlockingResearch is null)
            {
                result = $@"{{{{Blueprint
| name = {item.Name}
| color = {color}
| description = {item.Properties!["description"]}
| level = 1
| value = ???
| sell value = ???
| new items = {item.Name}
| vendors = TODO
}}}}";
            }
            else
            {
                var research = repository.GetDataItemByStringId(researchable.UnlockingResearch.StringId!);
                int cost = (int)research.Values["money"];

                var requirements = research.GetReferenceItems(repository, "requirements");
                var newBuildings = research.GetReferenceItems(repository, "enable buildings");
                var newItems = research.ReferenceCategories.Values
                    .Where(cat => cat.Key.StartsWith("enable"))
                    .SelectMany(cat => cat.Values)
                    .Select(reference => repository.GetDataItemByStringId(reference.TargetId))
                    .Except(newBuildings);
                var costs = research.GetReferences("cost")
                    .ToDictionary(reference => reference, reference => repository.GetDataItemByStringId(reference.TargetId));

                if (cost != 0)
                {
                    result = $@"{{{{Blueprint
| name = {research.Name}
| color = {color}
| description = {research.Values["description"]}
| level = {research.Values["level"]}
| value = {cost}
| sell value = {cost / 4}
| prerequisites = {string.Join(", ", requirements.Select(req => $"[[{req.Name}]]"))}
| new items = {string.Join(", ", newItems.Concat(newBuildings).Select(newItem => $"[[{newItem.Name}]]"))}
| vendors = TODO
}}}}";
                }
                else
                {
                    result = $@"{{{{Research
| name = {research.Name}
| description = {research.Values["description"]}
| estimated_time = {research.Values["time"]} hours
| level = {research.Values["level"]}
| costs = {string.Join(", ", costs.Select(pair => $"{pair.Key.Value0} [[{pair.Value.Name}]]"))}
| new_item(s) = {string.Join(", ", newItems.Select(item => $"[[{item.Name}]]"))}
| new_building(s) = {string.Join(", ", newBuildings.Select(item => $"[[{item.Name}]]"))}
}}}}";
                }
            }

            File.WriteAllText(Path.Combine(directory, "blueprint.txt"), result);
        }

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