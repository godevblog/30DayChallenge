using System;
using System.Reactive.Linq;
using RXLib;
using RXLib.Extensions;
using RXLib.Factories;

namespace CombineLatest
{
	class Program
	{
		static void Main()
		{
			var observableStream1 = GeneratorFactory.CreateGenerator(0, 100, 1, 500);
			var observableStream2 = GeneratorFactory.CreateGenerator(1000, 1100, 1, 2000);
			var observableStream3 = GeneratorFactory.CreateGenerator(10000, 11000, 1, 250);
			var observableProvider = new ObservableProvider();

			observableProvider.ConsoleKey.BreakWhenKey(ConsoleKey.Enter);

			var combineLatestStream = observableStream1.CombineLatest(
				observableStream2,
				observableStream3,
				observableProvider.ConsoleKey,
				(a, b, c, d) => new
				{
					A = a,
					B = b,
					C = c,
					D = d.Key
				});

			var combineLatestSubscribent = combineLatestStream.DefaultPrint("CombineLatest");

			Console.WriteLine("Pres any key to show results.");

			observableProvider.ConsoleKey.Start();

			observableProvider.Dispose();
			combineLatestSubscribent.Dispose();
		}
	}
}
