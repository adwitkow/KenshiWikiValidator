using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator
{
    public static class DataItemExtensions
    {
        public static decimal GetDecimal(this DataItem item, string key)
        {
            return Convert.ToDecimal(item.Values[key]);
        }

        public static int GetInt(this DataItem item, string key)
        {
            return Convert.ToInt32(item.Values[key]);
        }
    }
}
