using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSide.Service
{
    internal static class TicketService
    {
        public static void PurchaseTicket(string userEmail, TicketType ticket)
        {
            string qrCodePath = GetNewQrCodePath(userEmail);
            GenerateQrCode(qrCodePath);

            PurchasedTicket newTicket = new()
            {
                FromTime = DateTime.Now,
                ToTime = CalculateTimeDelta(DateTime.Now, ticket.Duration),
                Type = ticket.Tariff,
                QRCodePath = qrCodePath
            };

        }
        private static string GetNewQrCodePath(string userEmail)
        {
            return "";
        }

        private static DateTime CalculateTimeDelta(DateTime start, string duration)
        {
            return DateTime.Now;
        }

        private static void GenerateQrCode(string path)
        {
            Thread.Sleep(1000);
        }
    }
}