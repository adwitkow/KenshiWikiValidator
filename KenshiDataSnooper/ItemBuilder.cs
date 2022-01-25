using System.Diagnostics;
using KenshiDataSnooper.Builders;
using KenshiDataSnooper.Builders.Components;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper
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

            this.weaponBuilder = new WeaponBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintLocationsConverter,
                unlockingResearchConverter);
            this.armourBuilder = new ArmourBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintLocationsConverter,
                unlockingResearchConverter);

            this.longestItem = null!;
            this.longestTime = TimeSpan.Zero;
        }

        public IEnumerable<IItem> BuildItems()
        {
            var items = this.itemRepository.GetDataItemsByTypes(ItemType.Weapon, ItemType.Armour);

            var results = new List<IItem>();

            Parallel.ForEach(items, item =>
            {
                var built = this.BuildItem(item);
                results.Add(built);
            });

            Console.WriteLine();
            Console.WriteLine($"The longest item to build was {this.longestItem.Name} and took {this.longestTime}");

            return results;
        }

        private IItem BuildItem(DataItem item)
        {
            Console.WriteLine($"Building {item.Name}");
            var sw = Stopwatch.StartNew();

            IItem result = item.Type switch
            {
                ItemType.Weapon => this.weaponBuilder.Build(item),
                ItemType.Armour => this.armourBuilder.Build(item),
                _ => throw new ArgumentException($"ItemType {item.Type} cannot be converted", nameof(item)),
            };

            Console.WriteLine($"Built {item.Name} in {sw.Elapsed}");

            if (sw.Elapsed > this.longestTime)
            {
                this.longestTime = sw.Elapsed;
                this.longestItem = item;
            }

            return result;
        }
    }
}
