using KenshiDataSnooper;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders.Components
{
    internal class BlueprintLocationsConverter
    {
        private readonly IItemRepository itemRepository;

        public BlueprintLocationsConverter(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IEnumerable<ItemReference> Convert(DataItem baseItem, string categoryName)
        {
            var results = new List<ItemReference>();

            var referencingVendorLists = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.VendorList)
                .ToList();
            var blueprintVendorLists = referencingVendorLists
                .Where(list => list.GetReferences(categoryName)
                    .Any(cat => cat.TargetId.Equals(baseItem.StringId)))
                .ToList();
            var squads = blueprintVendorLists
                .SelectMany(vendor => this.itemRepository
                    .GetReferencingDataItemsFor(vendor)
                    .Where(item => item.Type == ItemType.SquadTemplate))
                .ToList();

            foreach (var squad in squads)
            {
                var factions = this.itemRepository.GetReferencingDataItemsFor(squad)
                    .Where(item => item.Type == ItemType.Faction);

                if (factions.Any())
                {
                    foreach (var faction in factions)
                    {
                        if (results.Any(reference => reference.StringId == faction.StringId))
                        {
                            continue;
                        }

                        var reference = new ItemReference()
                        {
                            StringId = faction.StringId,
                            Name = $"{faction.Name} (Faction)",
                        };

                        results.Add(reference);
                    }
                }
                else
                {
                    var towns = this.itemRepository.GetReferencingDataItemsFor(squad)
                    .Where(item => item.Type == ItemType.Town);

                    foreach (var town in towns)
                    {
                        if (results.Any(reference => reference.StringId == town.StringId || town.Name.Contains("(override)")))
                        {
                            continue;
                        }

                        var reference = new ItemReference()
                        {
                            StringId = town.StringId,
                            Name = town.Name,
                        };

                        results.Add(reference);
                    }
                }
            }

            return results;
        }
    }
}
