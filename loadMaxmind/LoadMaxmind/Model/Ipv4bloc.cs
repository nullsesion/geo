﻿using System;
using System.Collections.Generic;
using System.Text;

namespace loadMaxmind.Model
{
    class Ipv4bloc
    {
        public Guid ID { get; set; }
        public string Network { get; set; }
        public long GeonameId { get; set; }
        public long? RegisteredCountryGeonameId { get; set; }
        public long? RepresentedCountryGeonameId { get; set; }
        public string IsAnonymousProxy { get; set; }
        public string IsSatelliteProvider { get; set; }
        public UInt32 IpMax { get; set; }
        public UInt32 IpMin { get; set; }
        
        public CountryLocation CountryLocation { get; set; }
    }
}
