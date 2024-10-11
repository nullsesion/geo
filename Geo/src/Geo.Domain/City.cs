namespace Geo.Domain;

public record City(
	LocationStrings CityName
	, string Subdivision1IsoCode
	, LocationStrings Subdivision1Name
	, string Subdivision2IsoCode
	, LocationStrings Subdivision2Name
	, string MetroCode
	, string TimeZone);