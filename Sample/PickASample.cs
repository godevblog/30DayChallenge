using System;
using System.Reactive.Linq;

namespace Sample
{
	public class PickASample : IDisposable
	{
		private IDisposable _simpleObservable;
		private int _seconds;

		public PickASample(IObservable<long> observable, int seconds)
		{
			_seconds = seconds;

			var timeSpan = TimeSpan.FromSeconds(seconds);

			_simpleObservable = observable
				.Sample(timeSpan)
				.Timestamp()
				.Subscribe(
					timestamp =>
					{
						Console.WriteLine($"Sample every {_seconds} seconds: {timestamp}.");
					},
					exception =>
					{
						Console.WriteLine(exception);
					},
					() =>
					{
						Console.WriteLine("Completed");
					});
		}

		public void Dispose()
		{
			_simpleObservable.Dispose();
		}
	}
}
