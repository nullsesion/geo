using System;
using System.Collections.Generic;
using System.Text;
using CommonTools;
using loadMaxmind.Model;

namespace loadMaxmind.BissnesLayer.Model
{
    static class CsvModelHelper
    {
        //CountryLocationCsv
        public static CountryLocation CountryLocationCsvToDb(this CountryLocationCsv countryLocationCsv)
        {
            return new CountryLocation()
            {
                ID = new Guid(),
                GeonameId = countryLocationCsv.geoname_id,
                LocaleCode = countryLocationCsv.locale_code,
                ContinentCode = countryLocationCsv.continent_code,
                ContinentName = countryLocationCsv.continent_name,
                CountryIsoCode = countryLocationCsv.country_iso_code,
                CountryName = countryLocationCsv.continent_name,
                IsInEuropeanUnion = countryLocationCsv.is_in_european_union,
            };
        }
        
        //Ipv4blocCsv
        public static Ipv4bloc Ipv4blocCsvToDb(this Ipv4blocCsv ipv4blocCsv)
        {
            return new Ipv4bloc()
            {
                ID = new Guid(),
                Network = ipv4blocCsv.network,
                GeonameId = ipv4blocCsv.geoname_id,
                RegisteredCountryGeonameId  = ipv4blocCsv.registered_country_geoname_id,
                RepresentedCountryGeonameId = ipv4blocCsv.represented_country_geoname_id,
                IsAnonymousProxy = ipv4blocCsv.is_anonymous_proxy,
                IsSatelliteProvider = ipv4blocCsv.is_satellite_provider,
                IpMax = ipv4blocCsv.network.GetIpMax(),
                IpMin = ipv4blocCsv.network.GetIpMin(),
            };
        }
    }
}
