using KenshiDataSnooper.Builders;
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

        public ItemBuilder(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            var itemSourcesCreator = new ItemSourcesCreator(itemRepository);
            this.weaponBuilder = new WeaponBuilder(itemRepository);
            this.armourBuilder = new ArmourBuilder(itemRepository, itemSourcesCreator);
        }

        public IEnumerable<IItem> BuildItems()
        {
            var items = this.itemRepository.GetDataItemsByTypes(ItemType.Weapon, ItemType.Armour);

            var results = new List<IItem>();

            Parallel.ForEach(items, item =>
            {
                Console.WriteLine($"Building {item.Name}");
                switch (item.Type)
                {
                    case ItemType.Weapon:
                        results.Add(this.weaponBuilder.Build(item));
                        break;
                    case ItemType.Armour:
                        results.Add(this.armourBuilder.Build(item));
                        break;
                    default:
                        throw new ArgumentException($"ItemType {item.Type} cannot be converted", nameof(item));
                }
            });

            return results;
        }
    }
}
