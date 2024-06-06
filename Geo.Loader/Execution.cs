using Geo.Application.IpLocations.Commands.Create;
using MediatR;
using System.Net;
using Geo.Loader.CSV;
using MediatR.NotificationPublishers;
using System.Text.RegularExpressions;

namespace Geo.Loader
{
	public class Execution
	{
		private readonly IMediator _mediator;

		public Execution(IMediator mediator)
		{
			_mediator = mediator;
		}

		public void Run(FileInfo file)
		{
			LoaderTools.ReadFile(file, Valid, Write);
			// "demogeoip.csv"
			/*
			CreateIpLocation createIpLocationDTO = new CreateIpLocation
			{
				Id = Guid.NewGuid(),
				Address = "1.0.0.0",
				Network = "1.0.0.255",
				IpMin = 16777216,
				IpMax = 16777471,
				CountryCode = "AU",
				CountryName = "Australia",
			};
			
			Task<Guid> сreateIpLocationGuid = _mediator.Send(createIpLocationDTO);
			сreateIpLocationGuid.Wait();
			*/
		}

		private bool Valid(string x)
		{
			Regex regex = new Regex("""^\d+\.\d+\d+\.\d+\.\d+,\d+\.\d+\d+\.\d+\.\d+,\d+,\d+,.+,.+$""");
			return regex.IsMatch(x);
		}

		private void Write(IEnumerable<string> x)
		{
			foreach (string item in x)
			{
				Task<Guid> сreateIpLocationGuid = _mediator.Send(LoaderTools.ConvertStrToIPLocation(item));
				сreateIpLocationGuid.Wait();
			}
		}
		/*
		private async void Write(IEnumerable<string> x)
		{
			foreach (string item in x)
			{
				Guid сreateIpLocationGuid = await _mediator.Send(LoaderTools.ConvertStrToIPLocation(item));
			}
		}
		 */
	}
}
