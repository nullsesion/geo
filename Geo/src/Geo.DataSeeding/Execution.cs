using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly IStepFinish _seeding;
		public Execution(IStepFinish seeding) => _seeding = seeding;

		public async Task Run(IConfiguration config) //async Task
		{
			await _seeding.RunStep(config);

		}
	}
}
