using Geo.DomainShared;

namespace Get.DomainShared.Tests
{
	[TestClass]
	public class UnitIPV4
	{
		[TestMethod]
		public void Try_Convert_IPV4_FromString_IntReturned()
		{
			//arrange
			string IP = "104.16.196.14";

			//act
			if (IP.TryIpV4ToInt(out Int32 number))
				Assert.AreEqual(0x_68_10_c4_0e, number);
			else
				Assert.Fail("an error occurred while parsing");
		}

		[TestMethod]
		public void Try_Convert_IPV4_FromString_UIntReturned()
		{
			//arrange
			string IP = "104.16.196.14";

			//act
			if (IP.TryIpV4ToInt(out UInt32 number))
				Assert.AreEqual<UInt32>(0x_68_10_c4_0e, number);
			else
				Assert.Fail("an error occurred while parsing");
		}

		[TestMethod]
		public void Try_Convert_IPV4_FromString_LessZeroReturned()
		{
			//arrange
			string IP = "150.16.196.14";

			//act
			if (IP.TryIpV4ToInt(out Int32 number))
				Assert.IsTrue(number < 0);
			else
				Assert.Fail("an error occurred while parsing");
		}

		[TestMethod]
		public void Try_IPV4_GetMaxMin_MaxMinReturned()
		{
			//1.0.1.0/24
			string IP = "1.0.1.0"; //1000000000000000100000000
			uint mask = 24; //0000000000000000011111111
			if (IP.TryIpV4GetMaxMinViaMask(24, out uint max, out uint min))
			{
				Assert.AreEqual<UInt32>(0b_1000000000000000111111111, max);
				Assert.AreEqual<UInt32>(0b_1000000000000000100000000, min);
			}
			else
				Assert.Fail("an error occurred while parsing");
		}

		[TestMethod]
		public void Try_Parse_IPV6_FromString_UsingIPV4_FalseReturned()
		{
			//arrange
			string IP = "2001:db8:3333:4444:5555:6666:7777:8888";

			//act
			if (!IP.TryIpV4ToInt(out Int32 number))
				Assert.IsTrue(true);
			else
				Assert.Fail("an error occurred while parsing");
		}

	}
}