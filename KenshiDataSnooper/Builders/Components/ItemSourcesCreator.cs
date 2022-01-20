using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Builders.Components
{
    public class ItemSourcesCreator
    {
        private readonly ItemRepository itemRepository;

        public ItemSourcesCreator(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public ItemSources Create(DataItem baseItem)
        {
            var itemSources = new ItemSources();
            itemSources = this.ConvertCharacterSources(baseItem, itemSources);
            itemSources = this.ConvertLocationSources(baseItem, itemSources);
            return itemSources;
        }

        private static bool IsItemTheOnlyOne(DataItem baseItem, IEnumerable<KeyValuePair<DataReference, DataItem>> clothingItemsInSlot)
        {
            return clothingItemsInSlot.Count() == 1 && clothingItemsInSlot.First().Value.Equals(baseItem);
        }

        private ItemSources ConvertLocationSources(DataItem baseItem, ItemSources itemSources)
        {
            var referencingVendorLists = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.VendorList);
            var squads = referencingVendorLists
                .SelectMany(vendor => this.itemRepository
                    .GetReferencingDataItemsFor(vendor)
                    .Where(item => item.Type == ItemType.SquadTemplate));

            foreach (var squad in squads)
            {
                var aiPackages = squad.GetReferences(this.itemRepository, "AI packages");

                var isShop = aiPackages.Any(package => package
                    .GetReferences(this.itemRepository, "Leader AI Goals")
                    .Where(reference => "Shopkeeper".Equals(reference.Name))
                    .Any());

                var towns = this.itemRepository.GetReferencingDataItemsFor(squad)
                    .Where(item => item.Type == ItemType.Town);

                foreach (var town in towns)
                {
                    var reference = new ItemReference()
                    {
                        StringId = town.StringId,
                        Name = town.Name,
                    };

                    if (itemSources.Shops.Any(shop => shop.StringId == town.StringId)
                        || itemSources.Loot.Any(loot => loot.StringId == town.StringId))
                    {
                        continue;
                    }

                    if (isShop)
                    {
                        itemSources.Shops.Add(reference);
                    }
                    else
                    {
                        itemSources.Loot.Add(reference);
                    }
                }
            }

            return itemSources;
        }

        private ItemSources ConvertCharacterSources(DataItem baseItem, ItemSources sources)
        {
            var referencingCharacters = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.Character && this.itemRepository.GetReferencingDataItemsFor(item).Any())
                .ToList(); // TODO: Squads which don't spawn anywhere?

            var slot = baseItem.Values["slot"];

            foreach (var character in referencingCharacters)
            {
                var clothingItemPairs = character.ReferenceCategories.Values
                    .First(cat => "clothing".Equals(cat.Name))
                    .Select(cat => cat.Value)
                    .ToDictionary(cat => cat, cat => this.itemRepository.GetDataItemByStringId(cat.TargetId));
                var clothingItemsInSlot = clothingItemPairs.Where(item => slot.Equals(item.Value.Values["slot"]));

                // -1 means that no item will spawn if this position is rolled
                // 0 is completely disregarded
                // so only values higher than 0 are interesting to us
                var spawnablePairs = clothingItemsInSlot.Where(pair => pair.Key.Value0 > 0);

                if (!spawnablePairs.Any(pair => baseItem.Equals(pair.Value)))
                {
                    continue;
                }

                var reference = new ItemReference()
                {
                    Name = character.Name,
                    StringId = character.StringId,
                };

                if (IsItemTheOnlyOne(baseItem, spawnablePairs))
                {
                    sources.AlwaysWornBy.Add(reference);
                }
                else
                {
                    sources.PotentiallyWornBy.Add(reference);
                }
            }

            return sources;
        }
    }
}
