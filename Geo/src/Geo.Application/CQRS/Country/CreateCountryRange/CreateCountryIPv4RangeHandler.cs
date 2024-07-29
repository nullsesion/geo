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
			ResponseEntity<CountryIPv4Range> countryIPv4Range = CountryIPv4Range.Create(
				request.Network,
				request.GeonameId,
				request.RegisteredCountryGeoNameId,
				request.RepresentedCountryGeoNameId,
				request.IsAnonymousProxy,
				request.IsSatelliteProvider,
				request.IsAnycast
			);
			if (!countryIPv4Range.IsSuccess)
			{
				return new ResponseEntity<string>()
				{
					ErrorDetail = countryIPv4Range.ErrorDetail,
					IsSuccess = false,
				};
			}

			await _countryIPv4Repository.InsertAsync(countryIPv4Range.Entity, cancellationToken);

			await _countryIPv4Repository.SaveChangesAsync();

			return new ResponseEntity<string>()
			{
				Entity = request.Network,
				IsSuccess = true,
			};
		}
	}
}
