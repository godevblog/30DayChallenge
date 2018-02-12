using System;
using System.Reactive;
using System.Reactive.Linq;

namespace StampAndInterval
{
	public static class RXExtensions
	{
		public static IObservable<TSource> RemoveTimeInterval<TSource>(this IObservable<TimeInterval<TSource>> source)
		{
			return source.Select(x => x.Value);
		}

		public static IObservable<TSource> RemoveTimestamp<TSource>(this IObservable<Timestamped<TSource>> source)
		{
			return source.Select(x => x.Value);
		}
	}
}
