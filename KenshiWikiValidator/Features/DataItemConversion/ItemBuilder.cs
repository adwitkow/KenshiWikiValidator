using System.Diagnostics;
using KenshiWikiValidator.Features.DataItemConversion.Builders;
using KenshiWikiValidator.Features.DataItemConversion.Builders.Components;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion
{
    public class ItemBuilder
    {
        private readonly ItemRepository itemRepository;
        private readonly WeaponBuilder weaponBuilder;
        private readonly ArmourBuilder armourBuilder;

        private DataItem longestItem;
        private TimeSpan longestTime;

        public ItemBuilder(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            var itemSourcesCreator = new ItemSourcesCreator(itemRepository);
            var blueprintLocationsConverter = new BlueprintLocationsConverter(itemRepository);
            var unlockingResearchConverter = new UnlockingResearchConverter(itemRepository);

            weaponBuilder = new WeaponBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintLocationsConverter,
                unlockingResearchConverter);
            armourBuilder = new ArmourBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintLocationsConverter,
                unlockingResearchConverter);

            longestItem = null!;
            longestTime = TimeSpan.Zero;
        }

        public IEnumerable<IItem> BuildItems()
        {
            var items = itemRepository.GetDataItemsByTypes(ItemType.Weapon, ItemType.Armour);

            var results = new List<IItem>();

            Parallel.ForEach(items, item =>
            {
                var built = BuildItem(item);
                results.Add(built);
            });

            Console.WriteLine();
            Console.WriteLine($"The longest item to build was {longestItem.Name} and took {longestTime}");

            return results;
        }

        private IItem BuildItem(DataItem item)
        {
            Console.WriteLine($"Building {item.Name}");
            var sw = Stopwatch.StartNew();

            IItem result = item.Type switch
            {
                ItemType.Weapon => weaponBuilder.Build(item),
                ItemType.Armour => armourBuilder.Build(item),
                _ => throw new ArgumentException($"ItemType {item.Type} cannot be converted", nameof(item)),
            };

            Console.WriteLine($"Built {item.Name} in {sw.Elapsed}");

            if (sw.Elapsed > longestTime)
            {
                longestTime = sw.Elapsed;
                longestItem = item;
            }

            return result;
        }
    }
}
