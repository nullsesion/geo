using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Services
{
	public class Install2Db:IInstall2Db
	{
		private readonly IStepPrepareDownload _prepareDownload;

		public Install2Db(IStepPrepareDownload prepareDownload) => _prepareDownload = prepareDownload;

		public void RunStep(IConfiguration config)
		{
			throw new NotImplementedException();
		}
	}
}
