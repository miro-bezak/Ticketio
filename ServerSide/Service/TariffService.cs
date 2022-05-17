using ServerSide.Data;
using System.Collections.Generic;

namespace ServerSide.Service
{
    internal class TariffService
    {
        public static string GetCurrentCity()
        {
            Tariff tariff = JsonService.ParseTariff();
            return tariff.City;
        }

        public static List<TicketType> GetSingleTickets()
        {
            Tariff tariff = JsonService.ParseTariff();
            return tariff.SingleTickets;
        }

        public static List<TicketType> GetTravelCards()
        {
            Tariff tariff = JsonService.ParseTariff();
            return tariff.TravelCards;
        }
    }
}
