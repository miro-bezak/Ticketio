using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    public class TicketType
    {
        public string Duration { get; set; }

        public TimeSpan TimeLength { get; set; }

        public PriceType PriceType { get; set; }

        public string Price { get; set; }
    }
}
