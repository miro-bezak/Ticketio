using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public static class Config
    {
        public static string TariffPath = Path.Combine(Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", "ServerSide", "Persistence", "BrnoTariff.json");
        public static string UserDataPath = Path.Combine(Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", "ServerSide", "Persistence", "UserData.json");
        public static string BaseQrPath = Path.Combine(Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", "ServerSide", "Persistence", "QrCodes");
    }
}
