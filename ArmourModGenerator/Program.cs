using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

const string ModName = "armour-reversal";
const string ModFileName = ModName + ".mod";

var installations = OcsDiscoveryService.Default.DiscoverAllInstallations();

if (installations.Count == 0)
{
    Console.WriteLine("Unable to find game");
    Console.ReadKey();
    return;
}

Installation installation;

if (installations.Count == 1)
{
    // One installation so use it
    installation = installations.Values.First();
}
else
{
    // Display the installations to the user
    var keys = installations.Keys.ToList();

    Console.WriteLine("Multiple installations found");

    for (var i = 0; i < keys.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {keys[i]}");
    }

    Console.Write("Please select which to use: ");

    // Get the user to chose
    var selection = keys[int.Parse(Console.ReadLine() ?? "1") - 1];

    installation = installations[selection];

    Console.WriteLine($"Using the {selection} installation");
}

Console.WriteLine();

Console.Write("Reading load order... ");

Console.WriteLine("done");
Console.WriteLine();

Console.Write("Loading data... ");

// Build mod
var header = new Header(1, "Quas", "Generated armours for revers-engineering price values");

var options = new OcsDataContexOptions(ModFileName,
    Installation: installation,
    Header: header,
    LoadGameFiles: ModLoadType.Base,
    ThrowIfMissing: false);

var context = OcsDataContextBuilder.Default.Build(options);

Console.WriteLine("done");
Console.WriteLine();

var items = new HashSet<DataItem>();

var allArmours = context.Items.Values.OfType(ItemType.Armour);
var coverageArmour = allArmours.First(armour => armour.StringId == "53299-rebirth.mod");
var coverageCategory = coverageArmour.ReferenceCategories["part coverage"];

var slots = new[] { Slot.Hat, Slot.Body, Slot.Legs, Slot.Shirt, Slot.Boots };
var id = context.LastId + 1;
foreach (var slot in slots)
{
    var sampleItem = allArmours.First(armour => (int)armour.Values["slot"] == (int)slot);
    foreach (var material in Enum.GetValues<MaterialType>())
    {
        foreach (var armourClass in Enum.GetValues<ArmourClass>())
        {
            var item = new DataItem(ItemType.Armour, context.LastId + 1, $"{slot}-{armourClass}-{material}", $"{id}-{ModName}");

            foreach (var value in sampleItem.Values)
            {
                item.Values.Add(value.Key, value.Value);
            }

            item.Values["material type"] = (int)material;
            item.Values["class"] = (int)armourClass;
            item.Values["relative price mult"] = 1f;
            item.Values["inventory footprint height"] = 1;
            item.Values["inventory footprint width"] = 1;

            item.ReferenceCategories.Add(coverageCategory);

            items.Add(item);
            context.Items.Add(item);
            id++;
        }
    }
}

var vendor = context.NewItem(ItemType.VendorList, "armour-permutations-vendor");
vendor.Values["items count"] = 500;

var clothingCategory = new DataReferenceCategory("clothing");
foreach (var item in items)
{
    clothingCategory.Add(new DataReference(item.StringId, 100, 0, 0));
}

vendor.ReferenceCategories.Add(clothingCategory);

var hubBlackmarketItem = context.Items.Values.First(item => item.StringId == "44422-rebirth.mod");
var vendorsCategory = hubBlackmarketItem.ReferenceCategories["vendors"];
vendorsCategory.Clear();
vendorsCategory.Add(new DataReference(vendor.StringId, 0, 0, 0));

context.Save();

Console.WriteLine("done");
Console.WriteLine();

// Remove this mod and then add to the end of the load order.
installation.EnabledMods.RemoveAll(s => s == ModFileName);
installation.EnabledMods.Add(ModFileName);

OcsIOService.Default.SaveEnabledMods(installation);

Console.WriteLine("Added mod to end of load order");

Console.Write("Press any key to exit...");
Console.ReadKey();

enum Slot
{
    Weapon,
    Back,
    Hair,
    Hat,
    Eyes,
    Body,
    Legs,
    None,
    Shirt,
    Boots,
    Gloves,
    Neck,
    Backpack,
    Beard,
    Belt,
}

enum MaterialType
{
    Cloth,
    Leather,
    Chain,
    MetalPlate,
}

enum ArmourClass
{
    Cloth,
    Light,
    Medium,
    Heavy,
}