using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace loadMaxmind.BissnesLayer
{
    class UnzipCsv
    {
        private string _path { get; set; }
        public UnzipCsv(string path)
        {
            _path = path;
        }

        public bool Do(string[] fileNameForExtraction)
        {
            if (!String.IsNullOrEmpty(_path) && File.Exists(_path) && _path.EndsWith("zip"))
                return UnZipFiles(_path, fileNameForExtraction);
            else
                return false;
        }

        private bool UnZipFiles(string zipPath, string[] fileNameForExtraction)
        {
            DirectoryInfo directoryInfo = new FileInfo(zipPath).Directory;
            if (directoryInfo != null)
            {
                string dirForExtract = directoryInfo.FullName;
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string file = new FileInfo(entry.FullName).Name;
                        if (fileNameForExtraction.Contains(file))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(dirForExtract, file));

                            entry.ExtractToFile(destinationPath,true);
                        }
                    }
                }

                return fileNameForExtraction
                    .Select(x => File.Exists(Path.GetFullPath(Path.Combine(dirForExtract, x))) == false).Any();
            }

            return false;
        }
    }
}
