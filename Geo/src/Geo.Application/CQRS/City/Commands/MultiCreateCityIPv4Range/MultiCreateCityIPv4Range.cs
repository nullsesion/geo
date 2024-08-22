using Geo.DomainShared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Geo.DomainShared;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4Range: IRequest<ResponseEntity<IEnumerable<string>>>
	{
		public required IEnumerable<ICityIPv4Range> CityIPv4Ranges { get; set; }
	}
}
