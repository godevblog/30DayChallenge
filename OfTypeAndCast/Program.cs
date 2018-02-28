using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RXLib.Extensions;
using RXLib.Factories;

namespace OfTypeAndCast
{
	class Program
	{
		static void Main(string[] args)
		{
			var subscribents = new List<IDisposable>();

			AddGeneratedStream(subscribents);

			ManualStream(subscribents);

			Console.ReadKey();

			subscribents.ForEach(dispose => dispose.Dispose());
		}

		private static void AddGeneratedStream(ICollection<IDisposable> subscribents)
		{
			var observableStream = GeneratorFactory.CreateGenerator(100, 200, 1, 50)
				.Select(item => (object) item);

			subscribents.Add(observableStream
				.OfType<int>()
				.DefaultPrint("OfType"));

			subscribents.Add(observableStream
				.Cast<int>()
				.DefaultPrint("Cast"));
		}

		private static void ManualStream(ICollection<IDisposable> subscribents)
		{
			var manualStream = new Subject<object>();

			subscribents.Add(manualStream
				.OfType<byte>()
				.DefaultPrint("OfType"));

			subscribents.Add(manualStream
				.Cast<byte>()
				.DefaultPrint("Cast"));

			for (byte i = 0; i < 127; i++)
			{
				manualStream.OnNext(i);
			}

			manualStream.OnNext("4");
			manualStream.OnNext("5");

			manualStream.OnCompleted();
		}
	}
}
