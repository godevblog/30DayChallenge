using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RXLib;
using RXLib.Extensions;
using RXLib.Factories;

namespace MetadataManipulation
{
	class Program
	{
		static void Main()
		{
			var subscribents = new List<IDisposable>();
			var observableProvider = new ObservableProvider();
			var observableStream = GeneratorFactory.CreateGenerator(0, 5, 1, 300);

			Console.WriteLine("Press any keys (Enter end to stream).");

			MetadataCreateGenerator(subscribents, observableStream);
			MetadataManual(subscribents);
			MetadataConsoleKey(subscribents, observableProvider);
			
			Console.ReadKey();

			subscribents.ForEach(subscribent => subscribent.Dispose());
		}

		private static void MetadataConsoleKey(ICollection<IDisposable> subscribents, ObservableProvider observableProvider)
		{
			subscribents.Add(
				observableProvider.ConsoleKey
					.Materialize()
					.DefaultPrint("MaterializeConsoleKey"));

			subscribents.Add(observableProvider.ConsoleKey
				.Materialize()
				.Dematerialize()
				.DefaultPrint("DematerializeConsoleKey"));

			subscribents.Add(observableProvider.ConsoleKey
				.Select(x => x.Key)
				.DefaultPrint("ConsoleKey"));

			observableProvider.ConsoleKey.BreakWhenKey(ConsoleKey.Enter);
			observableProvider.ConsoleKey.Start();
		}

		private static void MetadataCreateGenerator(ICollection<IDisposable> subscribents,IObservable<int> observableStream)
		{
			subscribents.Add(observableStream.Materialize()
				.DefaultPrint("MaterializeCreateGenerator"));

			subscribents.Add(observableStream
				.Materialize()
				.Dematerialize()
				.DefaultPrint("DematerializeCreateGenerator"));
		}

		private static void MetadataManual(ICollection<IDisposable> subscribents)
		{
			var source = new Subject<string>();

			source.Materialize()
				.DefaultPrint("MaterializeManual");

			source.Materialize()
				.Dematerialize()
				.DefaultPrint("DematerializeManual");

			source.OnNext("Tekst 1");
			source.OnNext("Tekst 2");

			source.OnError(new Exception("Wyjątek"));
			source.OnCompleted();
		}
	}
}
