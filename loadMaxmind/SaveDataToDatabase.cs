using System;
using System.Collections.Generic;
using System.Text;
using loadMaxmind.BissnesLayer.Model;
using loadMaxmind.Model;
using Microsoft.EntityFrameworkCore;

namespace loadMaxmind
{
    static class SaveDataToDatabase
    {
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
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (Ipv4blocCsv item in ipv4blocs)
                {
                    db.Ipv4bloc.Add(item.Ipv4blocCsvToDb());
                    Console.Write(".");
                }
                db.SaveChanges();
            }
            Console.WriteLine("");
            Console.WriteLine("write data to Ipv4bloc");
        }

        public static void SaveCountryLocations(IEnumerable<CountryLocationCsv> countryLocations)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (CountryLocationCsv item in countryLocations)
                {
                    db.CountryLocations.Add(item.CountryLocationCsvToDb());
                    Console.Write(".");
                }
                db.SaveChanges();
            }
            Console.WriteLine("");
            Console.WriteLine("write data to CountryLocations");
        }
    }
}
