using KenshiDataSnooper.Builders;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiDataSnooper.Builders
{
    internal class WeaponBuilder : IItemBuilder<Weapon>
    {
        private readonly ItemRepository itemRepository;

        public WeaponBuilder(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Weapon Build(DataItem baseItem)
        {
            return new Weapon()
            {
                Name = baseItem.Name,
                Properties = baseItem.Values,
                StringId = baseItem.StringId,
            };
        }
    }
}