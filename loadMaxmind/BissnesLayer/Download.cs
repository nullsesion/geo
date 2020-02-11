using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace loadMaxmind.BissnesLayer
{
    public class Download
    {
        private string _url { get; set; }
        private string _dir { get; set; }
        private string _intervalDownloadHours { get; set; }

        public Download(Config config)
        {
            _url = config.GetConfigByName(Config.UrlLoad);
            _dir = Path.Combine(Environment.CurrentDirectory, config.GetConfigByName(Config.DirectoryLoad));
            _intervalDownloadHours = config.GetConfigByName(Config.IntervalDownloadHours);
        }
        
        public string Do()
        {
            if (!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);

            return LastOrDownLoadFile();
            /*
            return downLoadFile();
            */
        }

        private string LastOrDownLoadFile()
        {
            DirectoryInfo d = new DirectoryInfo(_dir);
            FileInfo[] Files = d.GetFiles("*.zip"); 


            IEnumerable<FileInfo> lastFiles = Files
                .Where(x => new Regex(@"^\d{18}$").IsMatch(x.Name.Split('.').First()));

            if (lastFiles != null && lastFiles.Any())
            {
                //проверка интервала и отдача последнего файла
                long lastTics = lastFiles
                    .Select(x => x.Name.Split('.').First())
                    .Select(x => long.Parse(x)).Max();

                long ticsNow = DateTime.Now.Ticks;
                long ticsFromConfig = DateTime.MinValue.AddHours(double.Parse(_intervalDownloadHours)).Ticks;
                if (ticsNow - ticsFromConfig < lastTics)
                {
                    Console.WriteLine("use zip from cache");
                    //возвращаем скачанный архив
                    return Path.Combine(_dir, lastTics.ToString() + ".zip");
                }

            }

            return downLoadFile();
        }
        private string downLoadFile()
        {
            string fileName = Path.Combine(_dir, DateTime.Now.Ticks.ToString() + ".zip");
            Console.WriteLine("download zip");

            using (WebClient wc = new WebClient())
            {
                //сделать асинхронную скачку
                wc.DownloadFile(new System.Uri(_url), fileName);
                return fileName;
            }
            
        }
    }
}
