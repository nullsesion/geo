using System.Net.WebSockets;
using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange
{
	public class MultiCreateCountryRangeHandler: IRequestHandler<MultiCreateCountryRangeIRequest, Result>
	{
		private readonly ICountryRepository _countryRepository;

		public MultiCreateCountryRangeHandler(ICountryRepository countryRepository) =>
			_countryRepository = countryRepository;
		public async Task<Result> Handle(MultiCreateCountryRangeIRequest request, CancellationToken cancellationToken)
		{
			if (request.CountryIPv4Ranges.Any())
			{
				IEnumerable<CountryIPv4Range> countryIPv4Ranges = request.CountryIPv4Ranges
					.Select(x => CountryIPv4Range.Create(x))
					.Where(x => x.IsSuccess && x.Value != null)
					.Select(x => x.Value);

				_countryRepository.MultiInsertCountryIPv4RangeAsync(countryIPv4Ranges, cancellationToken);
				return Result.Success();
			}
			return Result.Failure("Error paste");
		}
	}
}
