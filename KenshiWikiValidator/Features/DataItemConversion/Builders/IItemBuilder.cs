using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
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
