namespace Geo.Domain;

public record Country(
	LocationStrings CountryName
	, string ContinentCode
	, LocationStrings ContinentName
	, string CountryIsoCode
	);