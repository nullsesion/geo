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
		public void Fail(string message = "")
		{
			AnsiConsole.MarkupLine("[red]Fail[/] " + message);
		}
		/*
		public bool Confirm(string message)
		{
			return AnsiConsole.Confirm(message);
		}
		*/
		public bool Confirm(string message, bool def)
		{
			return AnsiConsole.Confirm(message,def);
		}
		/*
		public T ShowAsk<T>(string question)
		{
			return AnsiConsole.Ask<T>(question);
		}
		*/
	}
}
