using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.CreateCountryRange
{
	public class CreateCountryIPv4RangeHandler: IRequestHandler<CreateCountryIPv4Range,ResponseEntity<string>>
	{
		private readonly ICountryIPv4Repository _countryIPv4Repository;

		public CreateCountryIPv4RangeHandler(ICountryIPv4Repository countryIPv4Repository) => _countryIPv4Repository = countryIPv4Repository;
		public async Task<ResponseEntity<string>> Handle(CreateCountryIPv4Range request, CancellationToken cancellationToken)
		{
			//add mapper
			await _countryIPv4Repository.InsertAsync(new CountryIPv4Range()
			{
				Network = request.Network,
				IpMin = request.IpMin,
				IpMax = request.IpMax,
				GeonameId = request.GeonameId,
				RegisteredCountryGeoNameId = request.RegisteredCountryGeoNameId,
				RepresentedCountryGeoNameId = request.RepresentedCountryGeoNameId,
				IsAnonymousProxy = request.IsAnonymousProxy,
				IsSatelliteProvider = request.IsSatelliteProvider,
				IsAnycast = request.IsAnycast, 

			}, cancellationToken);

			await _countryIPv4Repository.SaveChangesAsync();

			return new ResponseEntity<string>()
			{
				Entity = request.Network,
				IsSuccess = true,
			};
		}
	}
}
