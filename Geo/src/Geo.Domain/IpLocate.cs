using Geo.Domain.Shared;

namespace Geo.Domain;

public record IpLocate(
	 string LocaleCode
	, Coordinate Coordinate
	, Country Country
	, City? City
	, bool IsInEuropeanUnion
	);