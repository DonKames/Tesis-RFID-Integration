
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis_RFID_Integration.models
{
    internal class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
