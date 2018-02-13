
using System;
using System.Reactive.Linq;

namespace Generators
{
	public class RangeGenerator : IDisposable
	{
		private IDisposable _rangeSubscription;

		public RangeGenerator(int start, int count)
		{
			Console.WriteLine("RangeGenerator");
			Console.WriteLine("Timestamp\t\t\tInterval\t\tValue");

			var rangeSource = Observable.Range(start, count)
				.TimeInterval()
				.Timestamp();

			_rangeSubscription = rangeSource.Subscribe(
				item =>
				{
					Console.WriteLine("{0}\t{1}\t{2}", 
						item.Timestamp, 
						item.Value.Interval, 
						item.Value.Value);
				},
				exception =>
				{
					Console.WriteLine(exception.Message);
				},
				() =>
				{
					Console.WriteLine("Range generated");
				});
		}

		public void Dispose()
		{
			_rangeSubscription.Dispose();
		}
	}
}
