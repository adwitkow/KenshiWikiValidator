using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.SharedComponents
{
    public class BlueprintSquadsConverter
    {
        private readonly IItemRepository itemRepository;

        public BlueprintSquadsConverter(IItemRepository itemRepository)
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
                var referencingItems = this.itemRepository.GetReferencingDataItemsFor(squad);

                if (referencingItems.Any())
                {
                    results.Add(new ItemReference(squad.StringId, squad.Name));
                }
            }

            return results;
        }
    }
}
