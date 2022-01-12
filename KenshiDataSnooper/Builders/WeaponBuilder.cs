using KenshiDataSnooper.Builders;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiDataSnooper.Builders
{
    internal class WeaponBuilder : IItemBuilder<Weapon>
    {
        public Weapon Build(DataItem baseItem)
        {
            return new Weapon();
        }
    }
}