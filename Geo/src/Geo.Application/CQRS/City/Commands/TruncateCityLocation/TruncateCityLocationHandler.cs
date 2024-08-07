using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.TruncateCityLocation
{
	public class TruncateCityLocationHandler: IRequestHandler<TruncateCityLocation, ResponseEntity<bool>>
	{
		private readonly ICityLocationRepository _cityLocationRepository;

		public TruncateCityLocationHandler(ICityLocationRepository cityLocationRepository)
		{
			_cityLocationRepository = cityLocationRepository;
		}

		public async Task<ResponseEntity<bool>> Handle(TruncateCityLocation request, CancellationToken cancellationToken)
		{
			await _cityLocationRepository
			.TruncateAsync();
			await _cityLocationRepository.SaveChangesAsync();
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = true,
			};
		}
	}
}
