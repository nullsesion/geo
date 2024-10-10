using Geo.Application.CQRS.City.Commands.TruncateCityIPv4Range;
using Geo.Application.CQRS.City.Commands.TruncateCityLocation;
using Geo.Application.CQRS.Country.Commands.TruncateCountryLocation;
using Geo.Application.CQRS.Country.Commands.TruncateTable;
using Geo.DataSeeding.Interfaces;
using Geo.DataSeeding.Services.CSV;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Services
{
	public class Install2Db:IInstall2Db
	{
		private readonly IStepPrepareUnzip _stepPrepareUnzip;
		private readonly IMediator _mediator;
		private readonly CsvService _csvHelper;

		public Install2Db(IStepPrepareUnzip stepPrepareUnzip, IMediator mediator, CsvService csvHelper) 
			=> (_stepPrepareUnzip, _mediator, _csvHelper) = (stepPrepareUnzip, mediator, csvHelper);

		public async Task RunStep(IConfiguration config)
		{
			string tmpDir = _stepPrepareUnzip.RunStep(config);

			Dictionary<string, string> FileFragment = new Dictionary<string, string>()
			{
				{"GeoLite2CountryLocations" ,"GeoLite2-Country-Locations-en"},
				{"GeoLite2CityLocations"    ,"GeoLite2-City-Locations-en"},
				{"GeoLite2CityIPv4"         ,"GeoLite2-City-Blocks-IPv4"},
				{"GeoLite2CountryIPv4"      ,"GeoLite2-Country-Blocks-IPv4"},
			};

			var truncateCountryLocation = await _mediator.Send(new TruncateCountryLocation(), CancellationToken.None);
			var truncateCityLocation = await _mediator.Send(new TruncateCityLocation(), CancellationToken.None);
			var truncateCountryIPv4 = await _mediator.Send(new TruncateCountryIPv4(), CancellationToken.None);
			var truncateCityIPv4Range = await _mediator.Send(new TruncateCityIPv4Range(), CancellationToken.None);

			foreach (KeyValuePair<string, string> csv in FileFragment)
			{
				switch (csv.Key)
				{
					case "GeoLite2CountryIPv4":
						await _csvHelper.LoadGeoLite2CountryIPv4(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CountryLocations":
						await _csvHelper.LoadGeoLite2CountryLocations(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CityIPv4":
						await _csvHelper.GeoLite2CityBlocksIPv4(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CityLocations":
						await _csvHelper.GeoLite2CityLocations(csv.Value, tmpDir, _mediator);
						break;
				}
			}
		}
	}
}
