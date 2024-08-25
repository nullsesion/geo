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

		public void RunStep(IConfiguration config)
		{
			string tmpDir = _stepPrepareUnzip.RunStep(config);

			Dictionary<string, string> FileFragment = new Dictionary<string, string>()
			{
				{"GeoLite2CountryLocations" ,"GeoLite2-Country-Locations-en"},
				{"GeoLite2CountryIPv4"      ,"GeoLite2-Country-Blocks-IPv4"},
				{"GeoLite2CityLocations"    ,"GeoLite2-City-Locations-en"},
				{"GeoLite2CityIPv4"         ,"GeoLite2-City-Blocks-IPv4"},
			};

			Console.WriteLine(tmpDir);
			
			foreach (KeyValuePair<string, string> csv in FileFragment)
			{
				
				switch (csv.Key)
				{
					case "GeoLite2CountryIPv4":
						_csvHelper.LoadGeoLite2CountryIPv4(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CountryLocations":
						_csvHelper.LoadGeoLite2CountryLocations(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CityIPv4":
						_csvHelper.GeoLite2CityBlocksIPv4(csv.Value, tmpDir, _mediator);
						break;

					case "GeoLite2CityLocations":
						_csvHelper.GeoLite2CityLocations(csv.Value, tmpDir, _mediator);
						break;
				}
				
			}
		}
	}
}
