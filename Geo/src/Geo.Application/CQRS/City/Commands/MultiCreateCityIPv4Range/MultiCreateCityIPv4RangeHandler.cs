using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4RangeHandler: IRequestHandler<MultiCreateCityIPv4Range, ResponseEntity<IEnumerable<string>>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public MultiCreateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<IEnumerable<string>>> Handle(MultiCreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			if (request.CityIPv4Ranges.Any())
			{
				List<string> successList = new List<string>();
				foreach (ICityIPv4Range cityIPv4Request in request.CityIPv4Ranges)
				{
					ResponseEntity<CityIPv4Range> cityIPv4Range = CityIPv4Range.Create(cityIPv4Request);
					if (cityIPv4Range.IsSuccess)
					{
						await _cityIPv4Repository.InsertAsync(cityIPv4Range.Entity, cancellationToken);
						successList.Add(cityIPv4Range.Entity.Network);
					}
				}
				await _cityIPv4Repository.SaveChangesAsync();
				return new ResponseEntity<IEnumerable<string>>()
				{
					IsSuccess = true,
					Entity = successList,
				};
			}
			return new ResponseEntity<IEnumerable<string>>()
			{
				ErrorDetail = "Empty List",
				IsSuccess = false,
			};
		}
	}
}
