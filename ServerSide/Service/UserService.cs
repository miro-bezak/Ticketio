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
        private static readonly string _userDataPath = @"C:\Users\mirob\PV178\Ticketio\ServerSide\Persistence\UserData.json";

        public static bool Register(string userEmail, string password)
        {
            var users = ParseUserDataFromJson();
            if (users.Where(user => user.Email == userEmail).Any())
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

            string serializedUsers = JsonConvert.SerializeObject(users);
            File.WriteAllText(_userDataPath, serializedUsers);
            return true;
        }

        public static bool Authenticate(string userEmail, string password)
        {
            var users = ParseUserDataFromJson();
            var usersWithGivenEmail = users.Where(x => x.Email == userEmail);

            if (!usersWithGivenEmail.Any())
            {
                return false;
            }

            return usersWithGivenEmail.First().PasswordHash == GetSha256Hash(password);
        }

        private static List<User> ParseUserDataFromJson()
        {
            string userDataJson = File.ReadAllText(_userDataPath);
            return JsonConvert.DeserializeObject<List<User>>(userDataJson);
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
