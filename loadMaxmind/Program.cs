using System;
using CommonTools;
using loadMaxmind.BissnesLayer;

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
