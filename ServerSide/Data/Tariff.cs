using System.Collections.Generic;

namespace ServerSide.Data
{
    public class Tariff
    {
        public string City { get; set; }

        public List<TicketType> SingleTickets { get; set; }

        public List<TicketType> TravelCards { get; set; }
    }
}
