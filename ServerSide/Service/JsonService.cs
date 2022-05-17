using Newtonsoft.Json;
using ServerSide.Data;
using System.Collections.Generic;
using System.IO;

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

        public static void WriteObjectToFile(string path, object @object)
        {
            string serializedUsers = JsonConvert.SerializeObject(@object);
            File.WriteAllText(path, serializedUsers);
        }
    }
}
