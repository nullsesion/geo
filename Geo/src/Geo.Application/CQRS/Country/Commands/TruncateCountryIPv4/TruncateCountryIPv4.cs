﻿using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.TruncateTable
{
	public class TruncateCountryIPv4: IRequest<ResponseEntity<bool>>
	{

	}
}