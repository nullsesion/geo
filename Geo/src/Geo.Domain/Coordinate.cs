using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;

namespace Geo.Domain;

public record Coordinate
{
	private readonly double _longitude;
	private readonly double _latitude;

	private Coordinate(double longitude, double latitude)
	{
		_longitude = longitude;
		_latitude = latitude;
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