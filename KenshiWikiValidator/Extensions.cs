using KenshiWikiValidator.Features.DataItemConversion;
using OpenConstructionSet.Data.Models;

namespace KenshiDataSnooper
{
    public static class Extensions
    {
        public static bool IsReferencing(this DataItem item, string id)
        {
            return item.ReferenceCategories.Values
                .Any(cat => cat.Values
                    .Any(val => val.TargetId == id));
        }

        public static IEnumerable<DataItem> GetReferenceItems(this DataItem item, IItemRepository repository, string categoryName)
        {
            return item.GetReferences(categoryName)
                    .Select(cat => repository.GetDataItemByStringId(cat.TargetId));
        }

        public static IEnumerable<DataReference> GetReferences(this DataItem item, string categoryName)
        {
            var category = item.ReferenceCategories.Values
                .FirstOrDefault(cat => categoryName.Equals(cat.Name));

            if (category != null)
            {
                return category.Values;
            }

            return Enumerable.Empty<DataReference>();
        }

        public static decimal Normalize(this decimal value)
        {
            // What an ugly hack :) https://stackoverflow.com/questions/4525854/remove-trailing-zeros
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
