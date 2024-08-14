using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.TruncateCityLocation
{
	public class TruncateCityLocationHandler: IRequestHandler<TruncateCityLocation, ResponseEntity<bool>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;
		public TruncateCityLocationHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<bool>> Handle(TruncateCityLocation request, CancellationToken cancellationToken)
		{
			await _cityIPv4Repository
			.TruncateCityLocationAsync();
			await _cityIPv4Repository.SaveChangesAsync();
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = true,
			};
		}
	}
}
