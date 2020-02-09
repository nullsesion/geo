using System;
using System.Collections.Generic;
using System.Linq;
using CommonTools;
using loadMaxmind.BissnesLayer;
using loadMaxmind.BissnesLayer.Model;

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
            
            //bool isAllFilesExtract = true;
            if (isAllFilesExtract)
            {
                //работаем с csv
                Csv csv = new Csv(config);
                IEnumerable<Ipv4blocCsv> records = csv.DoIpv4blocs();
                
                if (records != null && records.Any())
                    foreach (Ipv4blocCsv item in records.Take(100))
                        Console.WriteLine("geoname_id {0}", item.geoname_id);

                IEnumerable<CountryLocationCsv> r = csv.DoCountryLocations();
                if(r!= null && r.Any())
                    foreach (CountryLocationCsv item in r.Take(10))
                        Console.WriteLine("geoname_id {0}", item.geoname_id);



            }
            else
                throw new Exception("error extract files");
            

            //извлекаем информацию из csv


            //загружаем базу данных с сайта
            //загружаем в базу
            uint ipmax = "1.1.1.0/24".GetIpMax();
            uint ipmin = "1.1.1.0/24".GetIpMin();
            Console.WriteLine(ipmax);
            Console.WriteLine(ipmin);
            Console.WriteLine("-------- end run load --------");
            Console.ReadKey();
        }

    }
}
