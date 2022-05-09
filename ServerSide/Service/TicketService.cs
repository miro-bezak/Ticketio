using Aspose.BarCode.Generation;
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
            PurchasedTicket newTicket = new()
            {
                FromTime = DateTime.Now,
                ToTime = CalculateTimeDelta(DateTime.Now, ticket.Duration),
                Type = ticket.Tariff,
                QRCodePath = GetNewQrCodePath(userEmail)
            };

            GenerateQrCode(newTicket.QRCodePath,
                $"{userEmail}\n{newTicket.FromTime}\n{newTicket.ToTime}\n{newTicket.Type}");

            var users = JsonService.ParseUserData();
            users
                .Where(u => u.Email == userEmail)
                .First()
                .PurchasedTickets
                .Add(newTicket);

        }
        private static string GetNewQrCodePath(string userEmail)
        {
            var users = JsonService.ParseUserData();
            var currentUserTickets = users
                .Where(u => u.Email == userEmail)
                .First()
                .PurchasedTickets;
            return Config.BaseQrPath + userEmail + "-" + currentUserTickets.Count.ToString() + ".png";
        }

        private static DateTime CalculateTimeDelta(DateTime start, string duration)
        {
            int durationNumber = int.Parse(duration.Split(' ').First());
            TimeSpan? delta;
            if (duration.EndsWith("minutes"))
            {
                // single ticket with duration in minutes
                delta = TimeSpan.FromMinutes(durationNumber);
            }
            else
            {
                // travel card with duration in months
                delta = TimeSpan.FromDays(durationNumber * 30);
            }

            return start + delta.Value;
        }

        private static void GenerateQrCode(string path, string textToEncode)
        {
            Thread.Sleep(1000);
            BarcodeGenerator generator = new(EncodeTypes.Aztec, textToEncode);
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;

            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}