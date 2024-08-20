using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepFinish
{
	public void RunStep(IConfiguration config);
}