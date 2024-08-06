using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4RangeHandler: IRequestHandler<MultiCreateCityIPv4Range, ResponseEntity<IEnumerable<string>>>
	{
		public Task<ResponseEntity<IEnumerable<string>>> Handle(MultiCreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
