using System;
using System.Collections.Generic;
using System.Text;

namespace loadMaxmind.Model
{
    public class Ipv4bloc
    {
        public Guid ID { get; set; }
        public string Network { get; set; }
        public string GeonameId { get; set; }
        public string RegisteredCountryGeonameId { get; set; }
        public string RepresentedCountryGeonameId { get; set; }
        public string IsAnonymousProxy { get; set; }
        public string IsSatelliteProvider { get; set; }
        public UInt32 IpMax { get; set; }
        public UInt32 IpMin { get; set; }
    }
}
