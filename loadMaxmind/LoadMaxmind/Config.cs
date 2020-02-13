using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace loadMaxmind
{
    public class Config
    {
        private static readonly string _configFileName = "appsettings.json";
        public static readonly string UrlLoad = "UrlLoad";
        public static readonly string DirectoryLoad = "DirectoryLoad";
        public static readonly string IntervalDownloadHours = "IntervalDownloadHours";
        public static readonly string Ipv4blocs = "Ipv4blocs";
        public static readonly string CountryLocation = "CountryLocation";
        public static readonly string ConnectionString = "ConnectionString";


        //private ConfigurationBuilder _configBuilder { get; set; }
        private static IConfigurationRoot _config { get; set; }

        private readonly string  _fileConfig = Path.Combine(Environment.CurrentDirectory, "config.json");
        private static Config _instans;
        private static Dictionary<string, string> _configDictionary;
        private Config()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile(_configFileName);
            IConfigurationRoot _config = configBuilder.Build();
            _config.GetConnectionString("DefaultConnection");
            /*
            //зачитываем config 
            string configRaw = File.ReadAllText(_fileConfig);
            _configDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(configRaw);
            */
        }

        static public Config getInstans()
        {
            if(_instans == null)
                _instans = new Config();
            return _instans;
        }

        public string GetSqlConnection()
        {
            return _config.GetConnectionString("DefaultConnection");
        }
        
        public string GetConfigByName(string name)
        {
            if(!_configDictionary.ContainsKey(name))
                throw new Exception("invalid config params");

            return _configDictionary[name];
        }
        

    }
}
