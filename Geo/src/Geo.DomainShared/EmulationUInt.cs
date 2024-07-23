using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.DomainShared
{
	public static class EmulationUInt
	{
		public static UInt32 ToUInt32(this int n)
		{
			var bytes = BitConverter.GetBytes(n);
			return BitConverter.ToUInt32(bytes);
		}
	}
}
