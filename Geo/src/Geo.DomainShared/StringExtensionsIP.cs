using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Geo.DomainShared
{
	public static class StringExtensionsIP
	{
		public static bool TryIpV4ToUInt32(this string ip, out UInt32 number)
		{
			number = 0;
			string ipAddrString = ip.Split('/')[0];
			if (IPAddress.TryParse(ipAddrString, out IPAddress iPAddress))
			{
				byte[] bytesIP = iPAddress
					.GetAddressBytes()
					.ToArray<Byte>();
				number = BitConverter.ToUInt32(bytesIP);
				return true;
			}

			return false;
		}
		public static bool TryIpV4ToInt32(this string ip, out Int32 number)
		{
			number = 0;
			string ipAddrString = ip.Split('/')[0];
			if (IPAddress.TryParse(ipAddrString, out IPAddress ipAddress))
			{
				byte[] bytesIP = ipAddress
					.GetAddressBytes()
					.ToArray<Byte>();
				number = BitConverter.ToInt32(bytesIP);
				return true;
			}

			return false;
		}
	}
}
