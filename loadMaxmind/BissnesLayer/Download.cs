using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace loadMaxmind.BissnesLayer
{
    public class Download
    {
        private string _url { get; set; }
        private string _dir { get; set; }

        public Download(Config config)
        {
            _url = config.GetConfigByName("UrlLoad");
            _dir = Path.Combine(Environment.CurrentDirectory, config.GetConfigByName("DirectoryLoad"));
        }

        
        public string Do()
        {
            if (!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
            return downLoadFile();
        }

        private string downLoadFile()
        {
            string fileName = Path.Combine(_dir, DateTime.Now.Ticks.ToString() + ".zip");

            using (WebClient wc = new WebClient())
            {
                //сделать асинхронную скачку
                wc.DownloadFile(new System.Uri(_url), fileName);
                return fileName;
            }
            
        }
    }
}
