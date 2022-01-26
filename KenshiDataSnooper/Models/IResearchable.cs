using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper.Models
{
    public interface IResearchable
    {
        ItemReference? UnlockingResearch { get; set; }

        IEnumerable<ItemReference>? BlueprintLocations { get; set; }
    }
}
