using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    public interface IItemBuilder
    {
        object Build(IItem baseItem);
    }
}