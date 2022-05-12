using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.OcsProxy
{
    public static class Extensions
    {
        public static bool ContainsItem<T>(this IEnumerable<ItemReference<T>> references, IItem item)
            where T : IItem
        {
            return references.Any(reference => ReferenceEquals(item, reference.Item));
        }
    }
}
