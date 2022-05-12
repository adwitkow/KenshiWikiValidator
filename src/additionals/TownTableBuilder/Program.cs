// See https://aka.ms/new-console-template for more information
using System.Text;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

var states = new List<string>() { "dead", "alive", "imprisoned" };

var repository = new ItemRepository();
repository.Load();

var towns = repository.GetItems<Town>();

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

void AppendTown(StringBuilder builder, Town town)
{
    var factions = town.Factions;
    var newFactionName = factions.FirstOrDefault().Item?.Name;
    var isBase = IsBaseTown(town);

    builder.AppendLine($"| {town.Name} ({town.StringId})");

    if (isBase)
    {
        builder.AppendLine($"| [[{town.Name}]]");
    }
    else
    {
        var baseTowns = FindBaseItems(town);
        var baseTown = baseTowns.Single();

        var oldFactionNames = baseTown.Factions.Select(factionRef => factionRef.Item.Name);

        string? subTitle;
        if (oldFactionNames.Any(oldFactionName => oldFactionName.Equals(newFactionName)))
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
            subTitle = factions.Any() ? factions.Single().Item.Name : "Destroyed";
        }

        builder.AppendLine($"| [[{baseTown?.Name}/{subTitle}]]");
    }

    var faction = factions.Any() ? string.Join(", ", factions.Select(f => $"[[{f.Item.Name}]]")) : "None";
    builder.AppendLine($"| {faction}");

    var states = UnwrapWorldStates(town);
    builder.AppendLine($"| {states}");

    var overrides = town.OverrideTown.Select(overrideRef => overrideRef.Item);
    var furtherOverrides = overrides.Any() ? string.Join(", ", overrides.Select(item => $"{item.Name} ({item.StringId})")) : "None";
    builder.AppendLine($"| {furtherOverrides}");
}

string UnwrapWorldStates(Town town)
{
    var results = new List<string>();
    var worldStates = town.WorldState
        .ToDictionary(
            reference => reference.Item,
            reference => Convert.ToBoolean(reference.Value0));

    if (!worldStates.Any())
    {
        return "None";
    }

    foreach (var pair in worldStates)
    {
        var state = pair.Key;
        var stateValue = pair.Value;

        var npcIsItems = state.NpcIs
            .ToDictionary(
                reference => reference.Item,
                reference => reference.Value0);
        var npcIsNotItems = state.NpcIsNot
            .ToDictionary(
                reference => reference.Item,
                reference => reference.Value0);
        var playerAllyItems = state.PlayerAlly
            .ToDictionary(
                reference => reference.Item,
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

        foreach (var faction in playerAllyItems.Select(itemPair => itemPair.Key.Name))
        {
            if (stateValue)
            {
                results.Add($"[[{faction}]] faction is allied to the player");
            }
            else
            {
                results.Add($"[[{faction}]] faction is not allied to the player");
            }
        }
    }

    return string.Join(", ", results);
}

bool IsBaseTown(Town item)
{
    return FindBaseItems(item).Any(baseItem => baseItem.StringId == item.StringId);
}

IEnumerable<Town> FindBaseItems(Town town)
{
    var baseItems = new[] { town }.AsEnumerable();
    var previousBaseItems = baseItems;
    while (baseItems is not null && baseItems.Any())
    {
        previousBaseItems = baseItems;

        foreach (var baseItem in baseItems)
        {
            var newBaseItems = repository.GetItems<Town>()
                .Where(item => item.OverrideTown
                    .Any(overrideRef => overrideRef.Item == baseItem))
                .Distinct()
                .ToList();

            if (!newBaseItems.Any())
            {
                return new[] { baseItem };
            }

            baseItems = newBaseItems;
        }
    }

    return previousBaseItems;
}