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
        private readonly SquadBuilder squadBuilder;
        private readonly Dictionary<ItemType, IItemBuilder> itemBuilders;

        private DataItem longestItem;
        private TimeSpan longestTime;

        public ItemBuilder(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            var itemSourcesCreator = new ItemSourcesCreator(itemRepository);
            var blueprintSquadsConverter = new BlueprintSquadsConverter(itemRepository);
            var unlockingResearchConverter = new UnlockingResearchConverter(itemRepository);

            this.weaponBuilder = new WeaponBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintSquadsConverter,
                unlockingResearchConverter);
            this.armourBuilder = new ArmourBuilder(
                itemRepository,
                itemSourcesCreator,
                blueprintSquadsConverter,
                unlockingResearchConverter);
            this.squadBuilder = new SquadBuilder(itemRepository);

            this.itemBuilders = new Dictionary<ItemType, IItemBuilder>()
            {
                { ItemType.Weapon, this.weaponBuilder },
                { ItemType.Armour, this.weaponBuilder },
                { ItemType.SquadTemplate, this.weaponBuilder },
                { ItemType.VendorList, this.weaponBuilder },
            };

            this.longestItem = null!;
            this.longestTime = TimeSpan.Zero;
        }

        public IEnumerable<IItem> BuildItems()
        {
            var validTypes = this.itemBuilders.Keys.ToArray();
            var items = this.itemRepository.GetDataItemsByTypes(validTypes);

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

            var builder = this.itemBuilders[item.Type];

            var built = builder.Build(item);

            if (built is not IItem result)
            {
                throw new InvalidOperationException($"Registered an {nameof(ItemBuilder)} that does not return objects of type {nameof(IItem)}");
            }

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
