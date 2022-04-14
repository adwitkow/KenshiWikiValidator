using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    public abstract class ItemBuilderBase<T> : IItemBuilder
        where T : IDataItem
    {
        public abstract T Build(IItem baseItem);

        object IItemBuilder.Build(IItem baseItem)
        {
            return this.Build(baseItem);
        }
    }
}
