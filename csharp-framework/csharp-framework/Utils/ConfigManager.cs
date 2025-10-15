using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_framework.Utils
{
    public class Config
    {
        public string BaseUrl { get; set; }
        public string ApiUrl { get; set; }
        public string Browser { get; set; }

        public bool Headless { get; set; }
        public int SlowMo { get; set; }

        public Credentials Credentials { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class ConfigManager
    {
        private static readonly Config _config;

        static ConfigManager()
        {
            try
            {
                var json = File.ReadAllText("config.json");
                _config = JsonConvert.DeserializeObject<Config>(json);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error loading configuration: {ex.Message}");
                throw new ApplicationException("Failed to load configuration file.", ex);
            }
        }

        public static Config Settings => _config;

    }
}
