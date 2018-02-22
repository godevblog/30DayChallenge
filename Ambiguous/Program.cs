
using System;
using System.Reactive.Linq;
using RXLib.Factories;

namespace Ambiguous
{
	class Program
	{
		static void Main(string[] args)
		{
			var observableGenerator1 = GeneratorFactory.CreateGenerator(0, 100, 1, 30);
			var observableGenerator2 = GeneratorFactory.CreateGenerator(1000, 1050, 1, 20);
			var observableGenerator3 = GeneratorFactory.CreateGenerator(10000, 10020, 1, 40);

			var observableAmbiguous = Observable.Amb(observableGenerator1, observableGenerator2, observableGenerator3);

			var subscribent = observableAmbiguous.Subscribe(
				item =>
				{
					Console.WriteLine($"Amb: {item}");
				},
				exception => Console.WriteLine(exception));

			Console.ReadKey();

			subscribent.Dispose();
		}
	}
}
