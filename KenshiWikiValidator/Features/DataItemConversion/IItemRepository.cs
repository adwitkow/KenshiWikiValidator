using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion
{
    public interface IItemRepository
    {
        IItem GetDataItemByStringId(string id);

        IEnumerable<IItem> GetDataItems();

        IEnumerable<IItem> GetDataItemsByType(ItemType type);

        IEnumerable<IItem> GetDataItemsByTypes(params ItemType[] types);

        IDataItem GetItemByStringId(string id);

        IEnumerable<IDataItem> GetItems();

        IEnumerable<IItem> GetReferencingDataItemsFor(IItem reference);

        IEnumerable<IItem> GetReferencingDataItemsFor(string itemId);

        Task Load();
    }
}