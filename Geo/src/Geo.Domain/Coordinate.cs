using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;

namespace Geo.Domain;

public record Coordinate
{
	public double Longitude { get; private set; }
	public double Latitude { get; private set; }

	private Coordinate(double longitude, double latitude)
	{
		Longitude = longitude;
		Latitude = latitude;
	}

	public static Result<Coordinate> Create(double longitude, double latitude)
	{
		if (-180 > longitude || longitude > 180)
			return Result.Failure<Coordinate>("invalid longitude, longitude must be between -180..180");

		if (-90 > latitude || latitude > 90)
			return Result.Failure<Coordinate>("invalid longitude, longitude must be between -90..90");

		return new Coordinate(longitude, latitude);
	}
}