using KenshiDataSnooper;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders.Components
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

        private static void ConvertWeaponSources(DataItem baseItem, ItemSources sources, DataItem character)
        {
            // value0 is quantity
            // value1 is slot
            // value2 is chance

            // 0 quantity or negative quantity makes the spawning calculations ignore that weapon entirely.
            // It is not factored into the chances, nor does it allow a chance to spawn with no weapons.
            var viableWeapons = character
                .GetReferences("weapons")
                .Where(weaponReference => weaponReference.Value0 > 0);

            if (!viableWeapons.Any(weapon => weapon.TargetId.Equals(baseItem.StringId)))
            {
                return;
            }

            var reference = new ItemReference()
            {
                Name = character.Name,
                StringId = character.StringId,
            };

            // On the chance value, this is a case of 0 = 100.
            // So if something with a 0 chance and a greater than 0 quantity is at the top of the list,
            // nothing else can spawn in that slot.
            var firstWeapon = viableWeapons.First();
            int firstWeaponChance = firstWeapon.Value2;
            if (firstWeaponChance == 0 || firstWeaponChance == 100)
            {
                if (firstWeapon.TargetId.Equals(baseItem.StringId))
                {
                    sources.AlwaysWornBy.Add(reference);
                    return;
                }
                else
                {
                    // never worn
                    return;
                }
            }

            sources.PotentiallyWornBy.Add(reference); // TODO: Verify this
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
                var aiPackages = squad.GetReferenceItems(this.itemRepository, "AI packages");

                var isShop = aiPackages.Any(package => package
                    .GetReferenceItems(this.itemRepository, "Leader AI Goals")
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
            List<DataItem>? referencingCharacters = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.Character && this.itemRepository.GetReferencingDataItemsFor(item).Any())
                .ToList(); // TODO: Squads which don't spawn anywhere?

            foreach (var character in referencingCharacters)
            {
                if (baseItem.Type == ItemType.Armour)
                {
                    this.ConvertArmourSources(baseItem, sources, character);
                }
                else if (baseItem.Type == ItemType.Weapon)
                {
                    ConvertWeaponSources(baseItem, sources, character);
                }
            }

            return sources;
        }

        private void ConvertArmourSources(DataItem baseItem, ItemSources sources, DataItem character)
        {
            var slot = baseItem.Values["slot"];

            var clothingItemPairs = character
                .GetReferences("clothing")
                .ToDictionary(cat => cat, cat => this.itemRepository.GetDataItemByStringId(cat.TargetId));
            var clothingItemsInSlot = clothingItemPairs.Where(item => slot.Equals(item.Value.Values["slot"]));

            // -1 means that no item will spawn if this position is rolled
            // 0 is completely disregarded
            // so only values higher than 0 are interesting to us
            var spawnablePairs = clothingItemsInSlot.Where(pair => pair.Key.Value0 > 0);

            if (!spawnablePairs.Any(pair => baseItem.Equals(pair.Value)))
            {
                return;
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
    }
}
