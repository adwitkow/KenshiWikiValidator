// See https://aka.ms/new-console-template for more information
using KenshiWikiValidator;
using KenshiWikiValidator.Features.DataItemConversion;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;
using System.Text;

var states = new List<string>() { "dead", "alive", "imprisoned" };

var repository = new ItemRepository();
repository.Load();

var towns = repository.GetDataItemsByType(ItemType.Town);

var builder = new StringBuilder();
builder.AppendLine("{| class=\"article-table\"");
builder.AppendLine("! FCS Name (String ID)");
builder.AppendLine("! Article Name");
builder.AppendLine("! Controlling Faction");
builder.AppendLine("! World States");
builder.AppendLine("! Further overrides");

foreach (var town in towns.OrderBy(town => town.Name))
{
    builder.AppendLine("|-");
    AppendTown(builder, town);
}

builder.AppendLine("|}");

Console.WriteLine(builder.ToString());

void AppendTown(StringBuilder builder, DataItem town)
{
    var factions = town.GetReferenceItems(repository, "faction");
    var newFactionName = factions.FirstOrDefault()?.Name;
    var isBase = IsBaseTown(town);

    builder.AppendLine($"| {town.Name} ({town.StringId})");

    if (isBase)
    {
        builder.AppendLine($"| [[{town.Name}]]");
    }
    else
    {
        var townReferences = repository.GetReferencingDataItemsFor(town).ToList();
        var baseTown = townReferences.FirstOrDefault(reference => IsBaseTown(reference));

        while (baseTown is null && townReferences.Any())
        {
            townReferences = townReferences.SelectMany(reference => repository.GetReferencingDataItemsFor(reference)).ToList();
            baseTown = townReferences.FirstOrDefault(reference => IsBaseTown(reference));
        }

        var oldFactionName = baseTown?.GetReferenceItems(repository, "faction").SingleOrDefault().Name;

        string subTitle;
        if (oldFactionName.Equals(newFactionName))
        {
            if (town.Name.ToLower().Contains("half destroyed"))
            {
                subTitle = "Half-destroyed";
            }
            else if (town.Name.ToLower().Contains("destroyed"))
            {
                subTitle = "Destroyed";
            }
            else if (town.Name.ToLower().Contains("malnourished"))
            {
                subTitle = "Malnourished";
            }
            else
            {
                subTitle = newFactionName;
            }
        }
        else
        {
            subTitle = factions.Any() ? factions.SingleOrDefault().Name : "Destroyed";
        }

        builder.AppendLine($"| [[{baseTown.Name}/{subTitle}]]");
    }

    var faction = factions.Any() ? string.Join(", ", factions.Select(f => $"[[{f.Name}]]")) : "None";
    builder.AppendLine($"| {faction}");

    var states = UnwrapWorldStates(town);
    builder.AppendLine($"| {states}");

    var overrides = town.GetReferenceItems(repository, "override town");
    var furtherOverrides = overrides.Any() ? string.Join(", ", overrides.Select(item => $"{item.Name} ({item.StringId})")) : "None";
    builder.AppendLine($"| {furtherOverrides}");
}

string UnwrapWorldStates(DataItem town)
{
    var results = new List<string>();
    var worldStates = town.GetReferences("world state")
        .ToDictionary(
            reference => repository.GetDataItemByStringId(reference.TargetId),
            reference => Convert.ToBoolean(reference.Value0));

    if (!worldStates.Any())
    {
        return "None";
    }

    foreach (var pair in worldStates)
    {
        var state = pair.Key;
        var stateValue = pair.Value;

        var npcIsItems = state.GetReferences("NPC is")
            .ToDictionary(
                reference => repository.GetDataItemByStringId(reference.TargetId),
                reference => reference.Value0);
        var npcIsNotItems= state.GetReferences("NPC is NOT")
            .ToDictionary(
                reference => repository.GetDataItemByStringId(reference.TargetId),
                reference => reference.Value0);
        var playerAllyItems = state.GetReferences("player ally")
            .ToDictionary(
                reference => repository.GetDataItemByStringId(reference.TargetId),
                reference => Convert.ToBoolean(reference.Value0));

        foreach (var itemPair in npcIsItems)
        {
            var npc = itemPair.Key;
            var value = itemPair.Value;

            if (stateValue)
            {
                results.Add($"[[{npc.Name}]] is {states[value]}");
            }
            else
            {
                var validStates = states.ToList();
                validStates.RemoveAt(value);

                results.Add($"[[{npc.Name}]] is {string.Join(" or ", validStates)}");
            }
        }

        foreach (var itemPair in npcIsNotItems)
        {
            var npc = itemPair.Key;
            var value = itemPair.Value;

            if (stateValue)
            {
                results.Add($"[[{npc.Name}]] is not {states[value]}");
            }
            else
            {
                var validStates = states.ToList();
                validStates.RemoveAt(value);

                results.Add($"[[{npc.Name}]] is not {string.Join(" or ", validStates)}");
            }
        }

        foreach (var itemPair in playerAllyItems)
        {
            var faction = itemPair.Key;
            var value = itemPair.Value;

            if (stateValue)
            {
                results.Add($"[[{faction.Name}]] faction is allied to the player");
            }
            else
            {
                results.Add($"[[{faction.Name}]] faction is not allied to the player");
            }
        }
    }

    return string.Join(", ", results);
}

bool IsBaseTown(DataItem item)
{
    return !repository.GetReferencingDataItemsFor(item)
        .Any(item => item.Type == ItemType.Town);
}