using System;
using System.Reactive.Linq;

namespace Concat
{
	class Program
	{
		static void Main()
		{
			var observableSequence = Observable.Range(0, 5);

			for (var i = 0; i < 10; i++)
			{
				observableSequence = observableSequence.Concat(CreateGenerator(0, i * 10, 2, 500));
				observableSequence = observableSequence.Concat(Observable.Range(0, 5));
			}

			observableSequence.Subscribe(Console.WriteLine);

			Console.ReadKey();
		}

		private static IObservable<int> CreateGenerator(int initialState, int conditionValue, int incrementValue, int periodValue)
		{
			var period = TimeSpan.FromMilliseconds(periodValue);

			var generator = Observable.Generate(
				initialState,
				condition => condition <= conditionValue,
				iterate => iterate + incrementValue,
				resultSelector => resultSelector,
				timeSelector => period
			);

			return generator;
		}
	}
}
