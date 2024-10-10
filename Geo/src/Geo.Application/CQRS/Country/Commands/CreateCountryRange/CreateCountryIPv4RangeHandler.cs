using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryRange
{
	public class CreateCountryIPv4RangeHandler : IRequestHandler<CreateCountryIPv4Range, Result>
	{
		private readonly ICountryRepository _countryRepository;

		public CreateCountryIPv4RangeHandler(ICountryRepository countryRepository) => _countryRepository = countryRepository;
		public async Task<Result> Handle(CreateCountryIPv4Range request, CancellationToken cancellationToken)
		{
			Result<CountryIPv4Range> countryIPv4Range = CountryIPv4Range.Create(request);

			if (countryIPv4Range.IsFailure)
			{
				return Result.Failure<CountryIPv4Range>(countryIPv4Range.Error);
			}

			await _countryRepository.InsertCountryIPv4RangeAsync(countryIPv4Range.Value, cancellationToken);

			await _countryRepository.SaveChangesAsync();

			return Result.Success();
		}
	}
}
