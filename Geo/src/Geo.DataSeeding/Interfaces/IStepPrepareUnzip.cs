using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepPrepareUnzip
{
	public void RunStep(IConfiguration config);
}