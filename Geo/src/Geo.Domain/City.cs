namespace Geo.Domain;

public record City(
	string CityName
	, string Subdivision1IsoCode
	, string Subdivision1Name
	, string Subdivision2IsoCode
	, string Subdivision2Name
	, string MetroCode
	, string TimeZone);