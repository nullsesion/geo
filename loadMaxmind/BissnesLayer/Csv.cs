using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using loadMaxmind.BissnesLayer.Model;

namespace loadMaxmind.BissnesLayer
{
    class Csv
    {
        private Config _config { get; set; }
        public Csv(Config config)
        {
            _config  = config;
        }

        public IEnumerable<Ipv4blocCsv> DoIpv4blocs()
        {
            string tmpDir = _config.GetConfigByName("DirectoryLoad");
            string csvFileName = _config.GetConfigByName("Ipv4blocs");
            string path = Path.Combine(Environment.CurrentDirectory, tmpDir, csvFileName);
            
            List<Ipv4blocCsv> items = new List<Ipv4blocCsv>();

            using (StreamReader reader = new StreamReader(path))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    Ipv4blocCsv item = csv.GetRecord<Ipv4blocCsv>();
                    items.Add(item);
                }
            }

            return items;
        }

        public IEnumerable<CountryLocationCsv> DoCountryLocations()
        {
            string tmpDir = _config.GetConfigByName("DirectoryLoad");
            string csvFileName = _config.GetConfigByName("CountryLocation");
            string path = Path.Combine(Environment.CurrentDirectory, tmpDir, csvFileName);

            List<CountryLocationCsv> items = new List<CountryLocationCsv>();

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    CountryLocationCsv item = csv.GetRecord<CountryLocationCsv>();
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
