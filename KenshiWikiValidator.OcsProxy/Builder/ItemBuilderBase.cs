using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.OcsProxy.Builder
{
    public abstract class ItemBuilderBase<T> : IItemBuilder
        where T : IItem
    {
        public abstract T Build(DataItem baseItem);

        object IItemBuilder.Build(DataItem baseItem)
        {
            return this.Build(baseItem);
        }
    }
}
