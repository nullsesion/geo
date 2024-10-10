namespace Geo.Domain;

public record IpLocate(
	 LocaleCode LocaleCode
	, Coordinate Coordinate
	, Country Country
	, City? City
	, bool IsInEuropeanUnion
	);