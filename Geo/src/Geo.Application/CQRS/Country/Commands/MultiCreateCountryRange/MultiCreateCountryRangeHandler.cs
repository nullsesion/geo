using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange
{
	public class MultiCreateCountryRangeHandler: IRequestHandler<MultiCreateCountryRangeIRequest, ResponseEntity<IEnumerable<string>>>
	{
		private readonly ICountryIPv4Repository _countryIPv4Repository;

		public MultiCreateCountryRangeHandler(ICountryIPv4Repository countryIPv4Repository) =>
			_countryIPv4Repository = countryIPv4Repository;
		public async Task<ResponseEntity<IEnumerable<string>>> Handle(MultiCreateCountryRangeIRequest request, CancellationToken cancellationToken)
		{
			if (request.CountryIPv4Ranges.Any())
			{
				List<string> successList = new List<string>();
				foreach (var countryIPv4RangeRequest in request.CountryIPv4Ranges)
				{
					ResponseEntity<CountryIPv4Range> countryIPv4Range = CountryIPv4Range.Create(countryIPv4RangeRequest);
					if (countryIPv4Range.IsSuccess)
					{
						successList.Add(countryIPv4Range.Entity.Network);
						await _countryIPv4Repository.InsertAsync(countryIPv4Range.Entity, cancellationToken);
					}
				}
				await _countryIPv4Repository.SaveChangesAsync();
			}
			return new ResponseEntity<IEnumerable<string>>()
			{
				ErrorDetail = "Empty List",
				IsSuccess = false,
			};
		}
	}
}
