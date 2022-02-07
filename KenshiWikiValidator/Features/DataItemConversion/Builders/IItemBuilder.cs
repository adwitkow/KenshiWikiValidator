using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    public interface IItemBuilder
    {
        object Build(DataItem baseItem);
    }
}