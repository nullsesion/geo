using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using loadMaxmind.BissnesLayer.Model;
using loadMaxmind.Model;
using Microsoft.EntityFrameworkCore;

namespace loadMaxmind
{
    static class SaveDataToDatabase
    {
        /*
        public static void TrucateCountryLocations()
        {
            Console.WriteLine("");
            Console.WriteLine("TRUNCATE TABLE  public.\"CountryLocations\"");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE  public.\"CountryLocations\"");
                db.SaveChanges();
            }
        }
        */
        public static void TrucateIpv4bloc()
        {
            Console.WriteLine("");
            Console.WriteLine("TRUNCATE TABLE  public.\"Ipv4bloc\"");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE  public.\"Ipv4bloc\"");
                db.SaveChanges();
            }
        }

        public static void SaveIpv4blocs(IEnumerable<Ipv4blocCsv> ipv4blocs)
        {
            int pageSize = (int) Math.Ceiling((decimal) (ipv4blocs.Count() / 100));
            long pages = ipv4blocs.Count() / pageSize;
            for (int i = 0; i <= pages; i++)
            {
                SaveIpv4blocsPage(ipv4blocs.Skip(i * pageSize + 1).Take(pageSize));

                Console.WriteLine("");
                Console.WriteLine("{0}%", i);
            }

            Console.WriteLine("");
            Console.WriteLine("write data to Ipv4bloc");
        }

        private static void SaveIpv4blocsPage(IEnumerable<Ipv4blocCsv> ipv4blocs)
        {
            int j = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (Ipv4blocCsv item in ipv4blocs)
                {
                    db.Ipv4bloc.Add(item.Ipv4blocCsvToDb());
                    if(++j % 10 == 0)
                        Console.Write(".");
                }
                db.SaveChanges();
            }
        }

        public static void SaveCountryLocations(IEnumerable<CountryLocationCsv> countryLocations)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (CountryLocationCsv item in countryLocations)
                {
                    //ищем обновляем или добавляем
                    if(db.CountryLocations.Any(x => x.GeonameId == item.CountryLocationCsvToDb().GeonameId))
                    {
                        CountryLocation countryLocation = db.CountryLocations.First(x => x.GeonameId == item.CountryLocationCsvToDb().GeonameId);
                        countryLocation = item.CountryLocationCsvToDb();
                    }
                    else
                    {
                        db.CountryLocations.Add(item.CountryLocationCsvToDb());
                    }

                    
                    Console.Write(".");
                }
                db.SaveChanges();
            }
            Console.WriteLine("");
            Console.WriteLine("write data to CountryLocations");
        }
    }
}
