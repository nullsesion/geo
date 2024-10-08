using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocationHandler: IRequestHandler<MultiCreateCountryLocation, Result>
	{
		private readonly ICountryRepository _countryRepository;

		public MultiCreateCountryLocationHandler(ICountryRepository countryRepository) => 
			_countryRepository = countryRepository;

		public async Task<Result> Handle(MultiCreateCountryLocation request, CancellationToken cancellationToken)
		{
			if (request.CountryLocations.Any())
			{
				bool res = await _countryRepository.MultiInsertCountryLocationAsync(request.CountryLocations, cancellationToken);
				return Result.Success();
			}

			return Result.Failure("List Empty");
		}
	}
}
