using Geo.Application.CQRS.Country.Commands.TruncateTable;
using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Application.CQRS.Country.Commands.TruncateCountryLocation
{
	public class TruncateCountryLocationHandler : IRequestHandler<TruncateCountryLocation, ResponseEntity<bool>>
	{
		private readonly ICountryLocationRepository _countryLocationRepository;

		public TruncateCountryLocationHandler(ICountryLocationRepository countryLocationRepository)
		{
			_countryLocationRepository = countryLocationRepository;
		}

		public async Task<ResponseEntity<bool>> Handle(TruncateCountryLocation request, CancellationToken cancellationToken)
		{
			await _countryLocationRepository.TruncateAsync();
			await _countryLocationRepository.SaveChangesAsync();
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = true,
			};
		}
	}
}
