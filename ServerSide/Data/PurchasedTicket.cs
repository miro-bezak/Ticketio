using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    public class PurchasedTicket
    {
        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public string Type { get; set; }

        public string QRCodePath { get; set; }
    }
}
