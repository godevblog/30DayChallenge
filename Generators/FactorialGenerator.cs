using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Generators
{
	public class FactorialGenerator : IDisposable
	{
		private IObservable<Timestamped<int>> _observable;
		private IDisposable _subscribeGenerator;

		private static ulong Factorial(ulong i) => i < 1 ? 1 : i * Factorial(i - 1);

		public FactorialGenerator()
		{
			Console.WriteLine("Factorial");

			var initialState = 1ul;

			var observable = Observable.Generate(
				initialState,
				condition => condition < 23,
				iterate => iterate + 1,
				resultSelector => Factorial(resultSelector),
				timeSelector => TimeSpan.FromMilliseconds(100)
				).TimeInterval();

			_subscribeGenerator = observable.Subscribe(
				item =>
				{
					Console.WriteLine("{0} -> {1}", item.Interval, item.Value);
				},
				exception =>
				{
					Console.WriteLine(exception);
				},
				() =>
				{
					Console.WriteLine("Complete generate Factorial.");
				});
		}

		public void Dispose()
		{
			_subscribeGenerator.Dispose();
		}
	}
}
