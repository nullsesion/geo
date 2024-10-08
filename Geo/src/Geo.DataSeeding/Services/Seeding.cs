﻿using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Services
{
	public class Seeding: IStepFinish
	{
		private readonly IInstall2Db _install2Db;
		public Seeding(IInstall2Db install2Db) => _install2Db = install2Db;

		public async Task RunStep(IConfiguration config)
		{
			await _install2Db.RunStep(config);
		}
	}
}
