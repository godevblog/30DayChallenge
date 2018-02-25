using System;
using System.Reactive.Linq;
using RXLib;
using RXLib.Extensions;
using RXLib.Factories;

namespace AndThenWhen
{
	class Program
	{
		static void Main()
		{
			var observableStream1 = GeneratorFactory.CreateGenerator(0, 100, 1, 100);
			var observableStream2 = GeneratorFactory.CreateGenerator(0, 100, 1, 60);
			var observableStream3 = GeneratorFactory.CreateGenerator(0, 100, 1, 120);
			var observableProvider = new ObservableProvider();

			observableProvider.ConsoleKey.BreakeWhenKey(ConsoleKey.Enter);

			var whenAndThenSequence = Observable.When(observableStream1
				.And(observableStream2)
				.And(observableStream3)
				.And(observableProvider.ConsoleKey)
				.And(observableProvider.TimeCounter)
				.Then((first, second, third, consoleKey, time) =>
						new {
							One = first,
							Two = second,
							Three = third,
							Key = consoleKey.Key,
							Time = time
						})
			);

			Console.WriteLine("Pres any key to show results.");
			var whenAndThenSubscribent = whenAndThenSequence.DefaultPrint("WhenAndThen");

			observableProvider.ConsoleKey.Start();

			observableProvider.Dispose();
			whenAndThenSubscribent.Dispose();
		}
	}
}
