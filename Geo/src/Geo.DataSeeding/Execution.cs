using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly IStepFinish _seeding;
		public Execution(IStepFinish seeding) => _seeding = seeding;

		public void Run(IConfiguration config) //async Task
		{
			_seeding.RunStep(config);

		}
	}
}
