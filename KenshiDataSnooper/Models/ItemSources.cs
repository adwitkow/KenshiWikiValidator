using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper.Models
{
    public class ItemSources
    {
        public ItemSources()
        {
            this.AlwaysWornBy = new List<ItemReference>();
            this.PotentiallyWornBy = new List<ItemReference>();
            this.Shops = new List<ItemReference>();
            this.Loot = new List<ItemReference>();
        }

        public ICollection<ItemReference> AlwaysWornBy { get; set; }

        public ICollection<ItemReference> PotentiallyWornBy { get; set; }

        public ICollection<ItemReference> Shops { get; set; }

        public ICollection<ItemReference> Loot { get; set; }
    }
}
