
using System;
using System.Reactive.Linq;

namespace Repeat
{
	class Program
	{
		static void Main(string[] args)
		{
			var twoSecondsTimeSpan = TimeSpan.FromSeconds(2);
			var fiveSecondsTimeSpan = TimeSpan.FromSeconds(2);

			var observableSequenceTenTimes =
				Observable.Range(1, 5)
				.Delay(fiveSecondsTimeSpan)
				.Repeat(10)
				.Subscribe(item =>
					{
						Console.WriteLine($"Only ten times: {item}");
					},
					exception => Console.WriteLine(exception));

			var observableSequenceContinually =
				Observable.Range(1, 5)
				.Delay(twoSecondsTimeSpan)
				.Concat(CreateGenerator(100, 200, 10, 500))
				.Delay(twoSecondsTimeSpan)
				.Concat(Observable.Range(100, 5))
				.Delay(twoSecondsTimeSpan)
				.Concat(CreateGenerator(1000000, 2000000, 10000, 100))
				.Delay(fiveSecondsTimeSpan)
				.Repeat()
					.Subscribe(item =>
						{
							Console.WriteLine($"Continually: {item}");
						},
						exception => Console.WriteLine(exception));

			Console.ReadKey();

			observableSequenceTenTimes.Dispose();
			observableSequenceContinually.Dispose();
		}

		private static IObservable<int> CreateGenerator(int initialState, int conditionValue, int incrementValue, int periodValue)
		{
			var period = TimeSpan.FromMilliseconds(periodValue);

			var observableStream = Observable.Generate(
				initialState,
				condition => condition <= conditionValue,
				iterate => iterate + incrementValue,
				resultSelector => resultSelector,
				timeSelector => period
			);

			return observableStream;
		}
	}
}
