using KenshiDataSnooper.Builders;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper
{
    public class ItemBuilder
    {
        private readonly WeaponBuilder weaponBuilder;
        private readonly ArmourBuilder armourBuilder;

        public ItemBuilder()
        {
            this.weaponBuilder = new WeaponBuilder();
            this.armourBuilder = new ArmourBuilder();
        }

        public IItem BuildFrom(DataItem baseItem)
        {
            return baseItem.Type switch
            {
                ItemType.Weapon => this.weaponBuilder.Build(baseItem),
                ItemType.Armour => this.armourBuilder.Build(baseItem),
                _ => throw new ArgumentException($"ItemType {baseItem.Type} cannot be converted", nameof(baseItem)),
            };
        }
    }
}
