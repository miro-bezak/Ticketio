using ServerSide.Data;
using ServerSide.Service;
using System.Collections.Generic;

namespace ServerSide
{
    public static class Facade
    {
        public static string GetCurrentCity()
        {
            return TariffService.GetCurrentCity();
        }

        public static void PurchaseTicket(string userEmail, TicketType ticket)
        {
            TicketService.PurchaseTicket(userEmail, ticket);
        }

        public static PurchasedTicket? GetCurrentTicket(string userEmail)
        {
            return TicketService.GetCurrentTicket(userEmail);
        }

        public static List<PurchasedTicket> GetTicketHistory(string userEmail)
        {
            return TicketService.GetAllUserTickets(userEmail);
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
