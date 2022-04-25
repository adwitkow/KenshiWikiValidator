using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.OcsProxy.Builder
{
    public interface IItemBuilder
    {
        object Build(DataItem baseItem);
    }
}