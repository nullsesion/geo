﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace loadMaxmind
{
    public class Config
    {
        private readonly string  _fileConfig = Path.Combine(Environment.CurrentDirectory, "config.json");
        private static Config _instans;
        private static Dictionary<string, string> _configDictionary;
        private Config()
        {
            //зачитываем config 
            string configRaw = File.ReadAllText(_fileConfig);
            _configDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(configRaw);
        }

        static public Config getInstans()
        {
            if(_instans == null)
                _instans = new Config();
            return _instans;
        }

        public string GetConfigByName(string name)
        {
            if(!_configDictionary.ContainsKey(name))
                throw new Exception("invalid config params");

            return _configDictionary[name];
        }

    }
}
