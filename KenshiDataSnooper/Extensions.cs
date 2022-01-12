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
    }
}
