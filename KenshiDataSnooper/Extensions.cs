using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper
{
    public static class Extensions
    {
        public static bool IsReferencing(this DataItem item, string id)
        {
            return item.ReferenceCategories.Values
                .Any(cat => cat.Values
                    .Any(val => val.TargetId == id));
            //return item.ReferenceCategories
            //    .Any(cat => cat.References
            //        .Any(reference => reference.TargetId == id));
        }
    }
}
