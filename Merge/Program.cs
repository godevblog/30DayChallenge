using System;
using System.Reactive.Linq;
using RXLib.Factories;
using RXLib;

namespace Merge
{
	class Program
	{
		static void Main()
		{
			var observableProvider = new ObservableProvider();
			var observableGenerator1 = GeneratorFactory.CreateGenerator(0, 100, 1, 100);
			var observableGenerator2 = GeneratorFactory.CreateGenerator(1000, 2000, 2, 600);
			var observableGenerator3 = GeneratorFactory.CreateGenerator(10000, 15000, 51, 1000);

			Console.WriteLine("Press any keys, enter to end ConsoleKey stream.");

			var observableMerge1 = Observable.Merge(
				observableProvider.ConsoleKey.Select(x => (int)x.Key),
				observableGenerator1,
				observableGenerator2,
				observableGenerator3
				);

			var consoleKeySubscribent = observableProvider.ConsoleKey.Subscribe(
				key =>
				{
					if (key.Key != ConsoleKey.Enter)
					{
						return;
					}

					observableProvider.ConsoleKey.Stop();

					Console.WriteLine("Press any key to stop program.");
				});

			var subscribent1 = observableMerge1.Subscribe(
				item =>
				{
					Console.WriteLine($"Merge1: {item}");
				},
				exception => Console.WriteLine(exception));


			var observableMerge2 = GeneratorFactory.CreateGenerator(-1000, 0, 1, 10);
			observableMerge2.Merge(observableGenerator1).Merge(observableGenerator2).Merge(observableGenerator3);

			var subscribent2 = observableMerge2.Subscribe(
				item =>
				{
					Console.WriteLine($"Merge2: {item}");
				},
				exception => Console.WriteLine(exception));

			observableProvider.ConsoleKey.Start();

			Console.ReadKey();

			subscribent1.Dispose();
			subscribent2.Dispose();
			consoleKeySubscribent.Dispose();
			observableProvider.Dispose();
		}
	}
}
