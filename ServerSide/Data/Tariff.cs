using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    internal class Tariff
    {
        public string City { get; set; }

        public List<TicketType> Tickets { get; set; }

        public List<TicketType> TravelCards { get; set; }
    }
}
