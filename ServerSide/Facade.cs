using ServerSide.Data;
using ServerSide.Service;
using System.Collections.Generic;

namespace ServerSide
{
    public static class Facade
    {
        /// <summary>
        /// Returns the name of the city of which the available tickets are loaded.
        /// </summary>
        public static string GetCurrentCity()
        {
            return TariffService.GetCurrentCity();
        }

        /// <summary>
        /// Executes a ticket purchase of the given ticket type for the user with the provided 
        /// email address. This consists of the generation of the purchased ticket's QR code 
        /// and is intentionally delayed to prevent buying tickets during the ticket inspection.
        /// Thus, it is recommneded to run this in separate thread from the GUI.
        /// </summary>
        public static void PurchaseTicket(string userEmail, TicketType ticket)
        {
            TicketService.PurchaseTicket(userEmail, ticket);
        }

        /// <summary>
        /// Return the currently valid ticket of a user identified with a given email address.
        /// If the user has no valid ticket, <c>null</c> is returned.
        /// </summary>
        public static PurchasedTicket? GetCurrentTicket(string userEmail)
        {
            return TicketService.GetCurrentTicket(userEmail);
        }

        /// <summary>
        /// Returns a list of all the purchased tickets for a user with given email address.  
        /// </summary>
        public static List<PurchasedTicket> GetTicketHistory(string userEmail)
        {
            return TicketService.GetAllUserTickets(userEmail);
        }

        /// <summary>
        /// Authenticates a user based on user's email and password. Returns true if the given
        /// credentials are valid.
        /// </summary>
        public static bool Authenticate(string userEmail, string password)
        {
            return UserService.Authenticate(userEmail, password);
        }

        /// <summary>
        /// Registers a new user into the system with the provided credentials. Returns false
        /// when a user with the given email is already registered in the system.
        /// </summary>
        public static bool Register(string userEmail, string password)
        {
            return UserService.Register(userEmail, password);
        }

        /// <summary>
        /// Return the list of available single tickets for the currenly loaded city.
        /// </summary>
        public static List<TicketType> GetSingleTickets()
        {
            return TariffService.GetSingleTickets();
        }

        /// <summary>
        /// Return the list of available travel cards for the currenly loaded city.
        /// </summary>
        public static List<TicketType> GetTravelCards()
        {
            return TariffService.GetTravelCards();
        }
    }
}
