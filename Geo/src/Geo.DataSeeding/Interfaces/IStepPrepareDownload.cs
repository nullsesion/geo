using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepPrepareDownload
{
	public List<string> RunStep(IConfiguration config);
}