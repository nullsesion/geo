using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.TruncateTable
{
	public class TruncateCountryIPv4Handler:IRequestHandler<TruncateCountryIPv4, ResponseEntity<bool>>
	{
		private readonly ICountryIPv4Repository _countryIPv4Repository;

		public TruncateCountryIPv4Handler(ICountryIPv4Repository countryIPv4Repository) => _countryIPv4Repository = countryIPv4Repository;
		public async Task<ResponseEntity<bool>> Handle(TruncateCountryIPv4 request, CancellationToken cancellationToken)
		{
			await _countryIPv4Repository.Truncate();
			//await _countryIPv4Repository.SaveChangesAsync();
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = true,
			};
		}

	}
}
