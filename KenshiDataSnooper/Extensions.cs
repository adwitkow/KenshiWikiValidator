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

        public static decimal Normalize(this decimal value)
        {
            // What an ugly hack :) https://stackoverflow.com/questions/4525854/remove-trailing-zeros
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
