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

        public static async Task PurchaseTicket(string userEmail, TicketType ticket)
        {
            await Task.Run(() => { });
        }

        public static PurchasedTicket? GetCurrentTicket(string userEmail)
        {
            return null;
        }

        public static bool Authenticate(string userEmail, string password)
        {
            return UserService.Authenticate(userEmail, password);
        }

        public static bool Register(string userEmail, string password)
        {
            return UserService.Register(userEmail, password);
        }

        public static List<TicketType> GetSingleTickets()
        {
            return TariffService.GetSingleTickets();
        }

        public static List<TicketType> GetTravelCards()
        {
            return TariffService.GetTravelCards();
        }
    }
}
