using OpenConstructionSet.Data;

namespace KenshiWikiValidator
{
    public static class ItemExtensions
    {
        public static decimal GetDecimal(this IItem item, string key)
        {
            return Convert.ToDecimal(item.Values[key]);
        }

        public static int GetInt(this IItem item, string key)
        {
            return Convert.ToInt32(item.Values[key]);
        }
    }
}
