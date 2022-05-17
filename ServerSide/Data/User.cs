using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerSide.Data
{
    public class User
    {
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<PurchasedTicket> PurchasedTickets { get; set; }
    }
}
