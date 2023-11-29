using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis_RFID_Integration.models
{
    //internal class Reading
    //{
    //}
    public class Reading
    {
        public string Type { get; set; }
        public string Antenna { get; set; }
        public string EPC { get; set; }
        public string RSSI { get; set; }
    }
}
