using Geo.DomainShared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Application.CQRS.Country.Commands.TruncateCountryLocation
{
	public class TruncateCountryLocation: IRequest<ResponseEntity<bool>>
	{
	}
}
