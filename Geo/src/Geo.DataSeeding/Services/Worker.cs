using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.DataSeeding.Services
{
	public class Worker
	{
		public string nameWorker { get; }
		public WorkerStatus status { get; set; } = WorkerStatus.Undefine;

		public Worker(string name)
		{
			nameWorker = name;
		}
	}
}
