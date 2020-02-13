using System;
using System.Collections.Generic;
using System.Text;

namespace loadMaxmind.BissnesLayer.Model
{
    class Ipv4blocCsv
    {
        public string network { get; set; }
        public string geoname_id { get; set; }
        public string registered_country_geoname_id { get; set; } 
        public string represented_country_geoname_id { get; set; }  
        public string is_anonymous_proxy { get; set; } 
        public string is_satellite_provider { get; set; }
    }
}
