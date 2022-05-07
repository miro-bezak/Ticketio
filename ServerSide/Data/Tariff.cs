using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    public class Tariff
    {
        public string City { get; set; }

        public List<TicketType> SingleTickets { get; set; }

        public List<TicketType> TravelCards { get; set; }
    }
}
