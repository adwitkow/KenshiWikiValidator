using KenshiDataSnooper;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders.Components
{
    internal class BlueprintLocationsConverter
    {
        private readonly ItemRepository itemRepository;

        public BlueprintLocationsConverter(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IEnumerable<ItemReference> Convert(DataItem baseItem, string categoryName)
        {
            var results = new List<ItemReference>();

            var referencingVendorLists = itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.VendorList)
                .ToList();
            var blueprintVendorLists = referencingVendorLists
                .Where(list => list.GetReferences(categoryName)
                    .Any(cat => cat.TargetId.Equals(baseItem.StringId)))
                .ToList();
            var squads = blueprintVendorLists
                .SelectMany(vendor => itemRepository
                    .GetReferencingDataItemsFor(vendor)
                    .Where(item => item.Type == ItemType.SquadTemplate))
                .ToList();

            foreach (var squad in squads)
            {
                var towns = itemRepository.GetReferencingDataItemsFor(squad)
                    .Where(item => item.Type == ItemType.Town);

                foreach (var town in towns)
                {
                    if (results.Any(reference => reference.StringId == town.StringId))
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

            return results;
        }
    }
}
