using Geo.DomainShared;

namespace Get.DomainShared.Tests
{
	[TestClass]
	public class UnitIPV4
	{
		[TestMethod]
		public void Try_Convert_IPV4_FromString_UIntReturned()
		{
			//arrange
			string IP = "62.118.93.157";

			//act
			if (IP.TryIpV4ToInt32(out Int32 number))
				Assert.AreEqual(1047944605, number);
			else
				Assert.Fail("bad parse");
		}
	}
}