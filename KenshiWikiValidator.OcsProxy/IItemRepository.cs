using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public interface IItemRepository
    {
        string? GameDirectory { get; }

        IEnumerable<IItem> GetItems();

        IEnumerable<T> GetItems<T>() where T : IItem;

        IItem GetItemByStringId(string id);

        T GetItemByStringId<T>(string id) where T : IItem;

        void Load();
    }
}