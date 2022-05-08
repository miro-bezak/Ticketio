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
    internal class TariffService
    {
        public static string GetCurrentCity()
        {
            string tariffJson = File.ReadAllText(Config.TariffPath);
            Tariff tariff = JsonConvert.DeserializeObject<Tariff>(tariffJson);
            return tariff.City;
        }
    }
}
