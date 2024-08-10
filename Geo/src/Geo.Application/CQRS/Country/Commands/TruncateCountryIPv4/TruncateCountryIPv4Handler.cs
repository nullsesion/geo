using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.TruncateTable
{
	public class TruncateCountryIPv4Handler:IRequestHandler<TruncateCountryIPv4, ResponseEntity<bool>>
	{
		private readonly ICountryRepository _countryRepository;

		public TruncateCountryIPv4Handler(ICountryRepository countryRepository) => _countryRepository = countryRepository;
		public async Task<ResponseEntity<bool>> Handle(TruncateCountryIPv4 request, CancellationToken cancellationToken)
		{
			await _countryRepository.TruncateCountryIPv4RangeAsync();
			await _countryRepository.SaveChangesAsync();
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = true,
			};
		}

	}
}
