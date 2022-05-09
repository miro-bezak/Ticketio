using Newtonsoft.Json;
using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
