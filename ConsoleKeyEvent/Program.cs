using System;
using RXLib;

namespace ConsoleKeyEvent
{
	class Program
	{
		static void Main()
		{
			var observableProvider = new ObservableProvider();

			observableProvider.ConsoleKey
				.Subscribe(consoleKey =>
			{
				Console.WriteLine(consoleKey.Key);

				if (consoleKey.Key == ConsoleKey.Enter)
				{
					observableProvider.ConsoleKey.Stop();
				}
			});

			observableProvider.ConsoleKey.Start();

			observableProvider.Dispose();
		}
	}
}
