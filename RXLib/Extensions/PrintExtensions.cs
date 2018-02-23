using System;

namespace RXLib.Extensions
{
	public static class PrintExtensions
	{
		public static IDisposable DefaultPrint<T>(this IObservable<T> source, string identify)
		{
			var subscription = source.Subscribe(
				item =>
				{
					Console.WriteLine("{0}\t\titem: {1}", identify, item);
				},
				exception =>
				{
					Console.WriteLine("{0}\t\texception: {1}", identify, exception);
				},
				() =>
				{
					Console.WriteLine("{0}\t\tcompleted", identify);
				});

			return subscription;
		}
	}
}
