using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis_RFID_Integration.models
{
    internal class Product
    {
        public int Id { get; set; }
        public string EPC { get; set; }
        public int WarehouseId{ get; set; }
        public int SkuId{ get; set; }
        public bool Active{ get; set; }
    }
}
