using System;
using System.Reactive.Linq;

namespace Delay
{
	internal class Spy : IDisposable
	{
		private string _spyName;
		private IDisposable _spySubscribe;

		public Spy(IObservable<long> observableInterval, int time, string spyName)
		{
			_spyName = spyName;

			var delay = TimeSpan.FromMilliseconds(time);
			_spySubscribe = observableInterval.Delay(delay)
				.Subscribe(
				index =>
				{
					Console.WriteLine($"{DateTime.UtcNow} {_spyName}");
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
			_spySubscribe?.Dispose();
		}
	}
}