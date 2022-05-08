using ServerSide.Data;
using ServerSide.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerSide
{
    public static class Facade
    {
        public static string GetCurrentCity()
        {
            return TariffService.GetCurrentCity();
        }

        public static async Task PurchaseTicket(string user, TicketType ticket)
        {
            await Task.Run(() => { });
        }

        public static PurchasedTicket? GetPurchasedTicket(string user)
        {
            return null;
        }

        public static bool Authenticate(string user, string password)
        {
            return false;
        }

        public static void Register(string user, string password)
        {
            return;
        }

        public static List<TicketType> GetSingleTickets()
        {
            return new();
        }

        public static List<TicketType> GetTravelCards()
        {
            return new();
        }
    }
}
