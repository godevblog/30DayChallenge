using System;
using RXLib;

namespace OwnObservable
{
	class Program
	{
		static void Main(string[] args)
		{
			var observableProvider = new ObservableProvider();

			observableProvider.TimeCounter.Subscribe(time =>
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(time);
			});

			observableProvider.ValentinesDay.Subscribe(valentinesDayMessage =>
			{
				Console.ForegroundColor = valentinesDayMessage.Color;
				Console.WriteLine(valentinesDayMessage.Message);
			});

			Console.ReadKey();

			observableProvider.Dispose();
		}
	}
}
