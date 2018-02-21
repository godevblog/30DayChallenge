
using System;
using System.Reactive.Linq;

namespace RXLib.Factories
{
	public static class GeneratorFactory
	{
		public static IObservable<int> CreateGenerator(int initialState, int conditionValue, int incrementValue, int periodValue)
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
