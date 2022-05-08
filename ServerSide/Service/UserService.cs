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

        public static void Register(string user, string password)
        {
            var users = ParseUserDataFromJson();
            User newUser = new()
            {
                Email = user,
                PasswordHash = GetSha256Hash(password),
                PurchasedTickets = new List<PurchasedTicket>()
            };
            users.Add(newUser);

            string serializedUsers = JsonConvert.SerializeObject(users);
            File.WriteAllText(_userDataPath, serializedUsers);
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
