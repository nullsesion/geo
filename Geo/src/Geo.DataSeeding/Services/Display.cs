using Spectre.Console;
using System.IO;

namespace Geo.DataSeeding.Services
{
	public class Display
	{
		public void ShowHead(string message)
		{
			var startLoad = new Rule($"[green]{message}[/]");
			startLoad.Justification = Justify.Left;
			AnsiConsole.Write(startLoad);
		}
		public void Ok(string message = "")
		{
			AnsiConsole.MarkupLine("[green]Ok[/] " + message);
		}

		public void Write(string message = "")
		{
			AnsiConsole.Write(message);
		}

		public void WriteLine(string message = "")
		{
			AnsiConsole.WriteLine(message);
		}

		public void Fail(string message = "")
		{
			AnsiConsole.MarkupLine("[red]Fail[/] " + message);
		}
		public bool Confirm(string message, bool def)
		{
			return AnsiConsole.Confirm(message,def);
		}
	}
}
