using Newtonsoft.Json;
using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Service
{
    internal class JsonService
    {
        public static Tariff ParseTariff()
        {
            string tariffJson = File.ReadAllText(Config.TariffPath);
            return JsonConvert.DeserializeObject<Tariff>(tariffJson);
        }

        public static List<User> ParseUserData()
        {
            string userDataJson = File.ReadAllText(Config.UserDataPath);
            return JsonConvert.DeserializeObject<List<User>>(userDataJson);
        }
    }
}
