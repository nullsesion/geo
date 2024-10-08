using CSharpFunctionalExtensions;
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
	public class TruncateCountryLocationHandler : IRequestHandler<TruncateCountryLocation, Result>
	{
		private readonly ICountryRepository _countryRepository;

		public TruncateCountryLocationHandler(ICountryRepository countryRepository)
		{
			_countryRepository = countryRepository;
		}

		public async Task<Result> Handle(TruncateCountryLocation request, CancellationToken cancellationToken)
		{
			await _countryRepository.TruncateCountryLocationAsync();
			await _countryRepository.SaveChangesAsync();
			return Result.Success();
		}
	}
}
