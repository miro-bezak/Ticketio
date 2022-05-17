using System;

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
