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
