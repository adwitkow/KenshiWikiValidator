using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper.Models
{
    public class Crafting
    {
        public string? Building { get; set; }

        public string? BaseMaterial { get; set; }

        public decimal BaseMaterialCost { get; set; }

        public decimal FabricsCost { get; set; }
    }
}
