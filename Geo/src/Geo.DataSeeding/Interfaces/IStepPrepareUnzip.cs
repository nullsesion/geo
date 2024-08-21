using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepPrepareUnzip
{
	public string RunStep(IConfiguration config);
}