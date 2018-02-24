using System;
using System.Reactive.Linq;

namespace RXLib.Extensions
{
	public static class PrintExtensions
	{
		public static IDisposable DefaultPrint<T>(this IObservable<T> source, string identify)
		{
			var subscription = source
				.Subscribe(
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

		public static IDisposable PrintWithTimestamp<T>(this IObservable<T> source, string identify)
		{
			var subscription = source
				.Timestamp()
				.Subscribe(
					item =>
					{
						Console.WriteLine("{0}\t\titem: {1}-->{2}", identify, item.Timestamp,  item.Value);
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

		public static IDisposable PrintWithTimeInterval<T>(this IObservable<T> source, string identify)
		{
			var subscription = source
				.TimeInterval()
				.Subscribe(
					item =>
					{
						Console.WriteLine("{0}\t\titem: {1}-->{2}", identify, item.Interval, item.Value);
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
