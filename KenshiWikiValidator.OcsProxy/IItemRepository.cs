using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public interface IItemRepository
    {
        string? GameDirectory { get; }

        DataItem GetDataItemByStringId(string id);

        IEnumerable<DataItem> GetDataItems();

        IEnumerable<DataItem> GetDataItemsByType(ItemType type);

        IEnumerable<DataItem> GetDataItemsByTypes(params ItemType[] types);

        IItem GetItemByStringId(string id);

        T GetItemByStringId<T>(string id) where T : IItem;

        IEnumerable<IItem> GetItems();

        IEnumerable<T> GetItems<T>() where T : IItem;

        IEnumerable<DataItem> GetReferencingDataItemsFor(DataItem reference);

        IEnumerable<DataItem> GetReferencingDataItemsFor(string itemId);

        void Load();
    }
}