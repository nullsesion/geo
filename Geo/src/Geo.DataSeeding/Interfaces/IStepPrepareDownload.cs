using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IStepPrepareDownload
{
	public void RunStep(IConfiguration config);
}