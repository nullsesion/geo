using Spectre.Console;

namespace Geo.DataSeeding.Services
{
	public class Display
	{
		private readonly IEnumerable<Worker> _workers;

		public Display(IEnumerable<Worker> workers)
		{
			_workers = workers;
		}

		public void ShowStatus()
		{
			Table table = new Table();

			table.AddColumn("Tasks");
			table.AddColumn(new TableColumn("Status").Centered());

			foreach (Worker item in _workers)
			{
				table.AddRow(item.nameWorker, $"[green]{item.status}[/]");
			}

			AnsiConsole.Write(table);
		}

		public T ShowAsk<T>(Markup question)
		{
			return AnsiConsole.Ask<T>("What's your [green]name[/]?");
			
		}
	}
}
