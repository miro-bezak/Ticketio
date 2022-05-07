using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    internal class TicketType
    {
        public string Duration { get; set; }

        public TimeSpan TimeLength { get; set; }

        public string Type { get; set; }

        public string Price { get; set; }
    }
}
