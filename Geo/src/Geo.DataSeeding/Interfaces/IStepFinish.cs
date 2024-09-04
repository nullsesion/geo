using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepFinish
{
	public Task RunStep(IConfiguration config);
}