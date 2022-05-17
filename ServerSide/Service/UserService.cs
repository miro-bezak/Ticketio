using Newtonsoft.Json;
using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Service
{
    internal class UserService
    {
        public static bool Register(string userEmail, string password)
        {
            var users = JsonService.ParseUserData();
            if (users.Where(u => u.Email == userEmail).Any())
            {
                return false;
            }

            User newUser = new()
            {
                Email = userEmail,
                PasswordHash = GetSha256Hash(password),
                PurchasedTickets = new List<PurchasedTicket>()
            };
            users.Add(newUser);

            JsonService.WriteObjectToFile(Config.UserDataPath, users);
            return true;
        }

        public static bool Authenticate(string userEmail, string password)
        {
            var users = JsonService.ParseUserData();
            var usersWithGivenEmail = users.Where(u => u.Email == userEmail);

            if (!usersWithGivenEmail.Any())
            {
                return false;
            }

            return usersWithGivenEmail.First().PasswordHash == GetSha256Hash(password);
        }

        private static string GetSha256Hash(string value)
        {
            StringBuilder stringBuilder = new();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(value));

                foreach (byte @byte in result)
                    stringBuilder.Append(@byte.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
