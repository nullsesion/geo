using System.Net;

namespace Geo.DomainShared
{
	public static class BinaryExtensionsForIP
	{
		public static bool TryIpV4ToInt(this string ip, out UInt32 number)
		{
			number = 0;
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				number = BitConverter.ToUInt32(GetBytesFromIP(iPAddress));
				return true;
			}

			return false;
		}
		public static bool TryIpV4ToInt(this string ip, out Int32 number)
		{
			number = 0;
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				number = BitConverter.ToInt32(GetBytesFromIP(iPAddress));
				return true;
			}

			return false;
		}
		

		public static bool TryIpV6ToInt(this string ip, out UInt128 number)
		{
			number = 0;
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				throw new NotImplementedException();
				return true;
			}

			return false;
		}
		public static bool TryIpV6ToInt(this string ip, out Int128 number)
		{
			number = 0;
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				throw new NotImplementedException();
				return true;
			}

			return false;
		}

		public static bool TryIpV4GetMaxMinViaMask(this string ip, int mask, out UInt32 max, out UInt32 min)
		{
			max = min = 0;
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				UInt32 bits = 0b_0000_0000_0000_0000_0000_0000_0000_0000;
				UInt32 currentBit = 0b_0000_0000_0000_0000_0000_0000_0000_0001;
				if (mask <= 32)
				{
					bits = bits | currentBit;
					for (int i = 32; i > mask; i--)
					{
						bits = bits | currentBit;
						currentBit = currentBit << 1;
					}
				}
				UInt32 number = BitConverter.ToUInt32(GetBytesFromIP(iPAddress));
				max = number | bits;
				min = number & ~bits;

				return true;
			}

			return false;
		}

		public static bool TryIpToBytes(this string ip, out byte[] bytes)
		{
			bytes = new byte[]{};
			if (IPAddress.TryParse(ip, out IPAddress iPAddress))
			{
				bytes = GetBytesFromIP(iPAddress);
				return true;
			}

			return false;
		}
		
		public static UInt32 ToUInt32(this int n)
		{
			var bytes = BitConverter.GetBytes(n);
			return BitConverter.ToUInt32(bytes);
		}

		private static byte[] GetBytesFromIP(IPAddress iPAddress)
		{
			byte[] bytesIP = iPAddress
				.GetAddressBytes()
				.Reverse()
				.ToArray<Byte>();
			return bytesIP;
		}
	}
}
