﻿using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Geo.Application.Common
{
    static public class StringExtensionsIPv4
    {
        private static bool IsIpAndMask(string ipMask)
        {
            var reg = new Regex(@"^\d+\.\d+\.\d+\.\d+/\d+$");
            return reg.IsMatch(ipMask);
        }
        private static bool IsIp(string ip)
        {
	        var reg = new Regex(@"^\d+\.\d+\.\d+\.\d+$");
	        if (!reg.IsMatch(ip))
	        {
                return false;
	        }
	        
	        foreach (string item in ip.Split('.'))
	        {
		        if (int.TryParse(item, out int n))
		        {
			        if (!(n < 256 && n > 0)) return false;
		        }
		        else return false;
	        }
            return true;
		}
		public static UInt32 GetIpMax(this string ipMask)
        {
            if (!IsIpAndMask(ipMask))
                throw new Exception();
            string[] splitIp = ipMask.Split('/');
            int maskLen = int.Parse(splitIp[1]);

            if (maskLen == 32)
                return ipMask.IpToUint32(); //32 битная маска 1 Ip в подсетке

            string endMask = new String('1', 32 - maskLen);
            
            UInt32 offset = Convert.ToUInt32(endMask, 2);
            UInt32 minIp = ipMask.GetIpMin();

            return minIp + offset;
        }

        public static UInt32 GetIpMin(this string ipMask)
        {
            if (!IsIpAndMask(ipMask))
                throw new Exception();
            string[] splitIp = ipMask.Split('/');
            string Ip = splitIp[0];
            int maskLen = int.Parse(splitIp[1]);
            if (maskLen == 32)
                return ipMask.IpToUint32(); //32 битная маска 1 Ip в подсетке

            string startMask = new String('1', maskLen);
            string endMask = new String('0', 32 - maskLen);
            UInt32 mask = Convert.ToUInt32(startMask + endMask, 2);
            UInt32 ipaddr = ipMask.IpToUint32();

            return mask & ipaddr;
        }

        public static UInt32 IpToUint32(this string ip)
        {
            byte[] bytsIP = IPAddress.Parse(ip.Split('/')[0]).GetAddressBytes().Reverse().ToArray<Byte>();
            UInt32 ipaddr = BitConverter.ToUInt32(bytsIP);
            return ipaddr;
        }
        
        public static bool TryIpToUint32(this string ip, out UInt32 number )
        {
	        number = 0;
			if (IsIp(ip))
	        {
		        byte[] bytsIP = IPAddress.Parse(ip.Split('/')[0]).GetAddressBytes().Reverse().ToArray<Byte>();
		        number = BitConverter.ToUInt32(bytsIP);
                return true;
			}

	        return false;
        }
    }
}

