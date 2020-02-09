using System;
using System.Collections.Generic;
using System.Text;

namespace loadMaxmind.BissnesLayer.Model
{
    class CountryLocationCsv
    {
        public string geoname_id { get; set; } 
        public string locale_code { get; set; } 
        public string continent_code { get; set; } 
        public string continent_name { get; set; } 
        public string country_iso_code { get; set; } 
        public string country_name { get; set; } 
        public string is_in_european_union { get; set; }
    }
}
