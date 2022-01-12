using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiDataSnooper.Builders
{
    internal interface IItemBuilder<T> : IItemBuilder
        where T : IItem
    {
        T Build(DataItem baseItem);
    }

    internal interface IItemBuilder
    {
    }
}
