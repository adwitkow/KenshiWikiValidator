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
            this.weaponBuilder = new WeaponBuilder(itemRepository);
            this.armourBuilder = new ArmourBuilder(itemRepository);
        }

        public IEnumerable<IItem> BuildItems()
        {
            var items = this.itemRepository.GetItemsByTypes(ItemType.Weapon, ItemType.Armour);

            var results = new List<IItem>();

            foreach (var item in items)
            {
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
            }

            return results;
        }
    }
}
