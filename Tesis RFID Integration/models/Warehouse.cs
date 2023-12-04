using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis_RFID_Integration.models
{
    internal class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
