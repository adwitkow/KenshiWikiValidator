using KenshiWikiValidator.Features.DataItemConversion;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator
{
    public static class Extensions
    {
        public static bool IsReferencing(this Item item, string id)
        {
            return item.ReferenceCategories
                .Any(cat => cat.References
                    .Any(val => val.TargetId == id));
        }

        public static IEnumerable<IItem> GetReferenceItems(this IItem item, IItemRepository repository, string categoryName)
        {
            return item.GetReferences(categoryName)
                    .Select(cat => repository.GetDataItemByStringId(cat.TargetId))
                    .ToList();
        }

        public static IEnumerable<IReference> GetReferences(this IItem item, string categoryName)
        {
            var category = item.ReferenceCategories
                .FirstOrDefault(cat => categoryName.Equals(cat.Name));

            if (category != null)
            {
                return category.References;
            }

            return Enumerable.Empty<Reference>();
        }

        public static decimal Normalize(this decimal value)
        {
            // What an ugly hack :) https://stackoverflow.com/questions/4525854/remove-trailing-zeros
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
