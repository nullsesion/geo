using CSharpFunctionalExtensions;

namespace Geo.Domain.Tests
{
	public class CoordinateTest
	{
		[Fact]
		public void Try_Create_Coordinate_CoordinateReturned()
		{
			double longitude = 2.9;
			double latitude = 61.5;

			Result<Coordinate> res = Coordinate.Create(longitude, latitude);

			//res.IsSuccess
			Assert.Equal(true, res.IsSuccess);
		}

		
		[Fact]
		public void Try_Create_GoodCoordinate_CoordinateReturned()
		{
			double longitude = -180;
			double latitude = -90;

			Result<Coordinate> res = Coordinate.Create(longitude, latitude);

			//res.IsSuccess
			Assert.Equal(true, res.IsSuccess);
		}

		[Fact]
		public void Try_Create_BadCoordinate_ErrorReturned()
		{
			double longitude = 180.1;
			double latitude = 90.5;

			Result<Coordinate> res = Coordinate.Create(longitude, latitude);

			//res.IsSuccess
			Assert.Equal(false, res.IsSuccess);
		}

		[Fact]
		public void Try_Create_BadLongitude_ErrorReturned()
		{
			double longitude = -180.1;
			double latitude = 90;

			Result<Coordinate> res = Coordinate.Create(longitude, latitude);

			Assert.Equal(true, res.IsFailure);
		}

		[Fact]
		public void Try_Create_BadLatitude_ErrorReturned()
		{
			double longitude = 180;
			double latitude = -90.5;

			Result<Coordinate> res = Coordinate.Create(longitude, latitude);

			Assert.Equal(true, res.IsFailure);
		}

	}
}