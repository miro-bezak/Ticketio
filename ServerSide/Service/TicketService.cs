using Aspose.BarCode.Generation;
using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
            JsonService.WriteObjectToFile(Config.UserDataPath, users);

        }

        public static List<PurchasedTicket> GetAllUserTickets(string userEmail)
        {
            var users = JsonService.ParseUserData();
            return users
                .Where(u => u.Email == userEmail)
                .First()
                .PurchasedTickets;
        }

        public static PurchasedTicket? GetCurrentTicket(string userEmail)
        {
            if (userEmail == null)
            {
                return null;
            }

            var validUserTickets = GetAllUserTickets(userEmail)
                .Where(ticket => ticket.ToTime > DateTime.Now);

            if (validUserTickets.Any())
            {
                return validUserTickets.First();
            }
            return null;
        }

        private static string GetNewQrCodePath(string userEmail)
        {
            var currentUserTickets = GetAllUserTickets(userEmail);
            return Path.Combine(Config.BaseQrPath, userEmail + "-" +
                currentUserTickets.Count.ToString() + ".png");
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
            Thread.Sleep(5000);
            BarcodeGenerator generator = new(EncodeTypes.Aztec, textToEncode);
            generator.Parameters.Barcode.XDimension.Millimeters = 1.75f;

            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}