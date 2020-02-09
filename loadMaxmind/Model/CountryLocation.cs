using System;
using System.Collections.Generic;
using System.Text;

namespace loadMaxmind.Model
{
    class CountryLocation
    {
        public Guid ID { get; set; }
        public string GeonameId { get; set; }
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryIsoCode { get; set; }
        public string CountryName { get; set; }
        public string IsInEuropeanUnion { get; set; }
    }
}
