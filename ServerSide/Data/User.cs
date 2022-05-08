﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Data
{
    public class User
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<PurchasedTicket> PurchasedTickets { get; set; }
    }
}