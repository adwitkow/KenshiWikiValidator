using System.Text.RegularExpressions;
using KenshiDataSnooper;
using KenshiDataSnooper.Models;
using Newtonsoft.Json;
using OpenConstructionSet.Models;

var repository = new ItemRepository();
var referenceResolver = new ReferenceResolver();
var stringIdRegex = new Regex(@"\d+-\w+\.\w+");

Console.WriteLine("Loading items...");
repository.Load();

var iconsDirectoryInfo = new DirectoryInfo(Path.Combine(repository.GameDirectory!, "data", "icons"));

var items = repository.GetItems();

WriteDetails(items, ItemType.Armour);
WriteDetails(items, ItemType.Weapon);

Console.WriteLine("Finished.");

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
        Directory.CreateDirectory(@$"{type}\{trimmedName}");
        File.WriteAllText($@"{type}\{trimmedName}\{trimmedName}-{item.StringId}.json", JsonConvert.SerializeObject(item, Formatting.Indented));
        Console.WriteLine($"Writing {trimmedName}...");

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

            var idItems = ids.Select(id => repository.GetDataItemByStringId(id));
            var names = idItems.Select(idItem => $"{idItem.Name}");
            var joined = string.Join(".", names);
            file.CopyTo(@$"{type}\{trimmedName}\icons\{item.StringId}.{joined}{file.Extension}");
        }
    }
}