﻿using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding.Interfaces;

public interface IInstall2Db
{
	public void RunStep(IConfiguration config);
}