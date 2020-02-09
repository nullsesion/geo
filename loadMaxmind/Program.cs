using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CommonTools;
using loadMaxmind.BissnesLayer;
using loadMaxmind.BissnesLayer.Model;
using loadMaxmind.Model;
using Microsoft.EntityFrameworkCore;

namespace loadMaxmind
{
    class Program
    {
        static void Main(string[] args)
        {

            Config config = Config.getInstans();
            
            Download download = new Download(config);
            string file = download.Do();
            
            UnzipCsv unzipCsv = new UnzipCsv(file);
            bool isAllFilesExtract = unzipCsv.Do(new []{config.GetConfigByName("Ipv4blocs"), config.GetConfigByName("CountryLocation") });
            
            if (isAllFilesExtract)
            {
                //работаем с csv
                Csv csv = new Csv(config);
                IEnumerable<Ipv4blocCsv> ipv4blocs = csv.DoIpv4blocs();
                IEnumerable<CountryLocationCsv> countryLocations = csv.DoCountryLocations();

                SaveDataToDatabase.TrucateCountryLocations();
                SaveDataToDatabase.SaveCountryLocations(countryLocations);

                SaveDataToDatabase.TrucateIpv4bloc();
                SaveDataToDatabase.SaveIpv4blocs(ipv4blocs);
            }
            else
                throw new Exception("error extract files");

            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

    }
}
