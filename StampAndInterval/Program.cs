using System;
using System.Reactive.Linq;

namespace StampAndInterval
{
	class Program
	{
		private static IDisposable _subscribeTimeInterval;
		private static IDisposable _subscribeTimestamp;
		private static IDisposable _subscribeWithoutTimeInterval;
		private static IDisposable _subscribeWithoutTimestamp;

		static void Main(string[] args)
		{
			InitializeInterval();
			InitializeIntervalWithoutTimeInterval();
			InitializeIntervalWithoutTimestamp();

			Console.ReadKey();

			ClearSubscribents();
		}

		private static void InitializeIntervalWithoutTimestamp()
		{
			var observableInterval = Observable.Interval(TimeSpan.FromSeconds(1)).Timestamp();

			_subscribeWithoutTimestamp = observableInterval
				.RemoveTimestamp()
				.Subscribe(
					x => Console.WriteLine("Remove Timestamp \t{0}", x)
				);
		}

		private static void InitializeIntervalWithoutTimeInterval()
		{
			var observableInterval = Observable.Interval(TimeSpan.FromSeconds(1)).TimeInterval();

			_subscribeWithoutTimeInterval = observableInterval
				.RemoveTimeInterval()
				.Subscribe(
					x => Console.WriteLine("Remove TimeInterval \t{0}", x)
				);
		}

		private static void InitializeInterval()
		{
			var observableInterval = Observable.Interval(TimeSpan.FromSeconds(1));

			_subscribeTimeInterval = observableInterval
				.TimeInterval()
				.Subscribe(
					x => Console.WriteLine("TimeInterval \t\t{0} \t{1}", x.Value, x.Interval)
				);

			_subscribeTimestamp = observableInterval
				.Timestamp()
				.Subscribe(
					x => Console.WriteLine("Timestamp \t\t{0} \t{1}", x.Value, x.Timestamp)
				);
		}

		private static void ClearSubscribents()
		{
			_subscribeTimeInterval.Dispose();
			_subscribeTimestamp.Dispose();
			_subscribeWithoutTimeInterval.Dispose();
			_subscribeWithoutTimestamp.Dispose();
		}
	}
}
