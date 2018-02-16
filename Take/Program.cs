using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Take
{
	class Program
	{
		private const int GenerateItemCount = 20;
		private static IObservable<int> _generator1;
		private static IObservable<int> _generator2;

		static void Main(string[] args)
		{
			InitializeGenerators();

			AddTakeLastSubscribent();

			AddTakeSubscribent();

			AddTakeWhileSubscribent();

			AddTakeUntilSubscribent();

			Console.ReadKey();
		}

		private static void InitializeGenerators()
		{
			var initialState = 0;

			_generator1 = Observable.Generate(
				initialState,
				condition => condition < GenerateItemCount,
				iterate => iterate + 1,
				resultSelector => resultSelector,
				timeSelector => TimeSpan.FromMilliseconds(100)
			);

			_generator2 = Observable.Generate(
				initialState,
				condition => condition < GenerateItemCount,
				iterate => iterate + 1,
				resultSelector => resultSelector,
				timeSelector => TimeSpan.FromMilliseconds(400)
			);
		}

		private static void AddTakeLastSubscribent()
		{
			_generator1
				.TakeLast(10)
				.Subscribe(
					item =>
					{
						Console.WriteLine($"TakeLast 10 items from {GenerateItemCount}: {item}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddTakeWhileSubscribent()
		{
			_generator1
				.TakeWhile(x => x < 5)
				.Subscribe(
					item =>
					{
						Console.WriteLine($"TakeWhile, show only some first generated data: {item}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddTakeSubscribent()
		{
			_generator1
				.Take(10)
				.Subscribe(
					item =>
					{
						Console.WriteLine($"Take (10): {item}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddTakeUntilSubscribent()
		{
			_generator2
				.TakeUntil(_generator1)
				.Subscribe(
					item =>
					{
						Console.WriteLine($"TakeUntil, generator1 publishing: {item}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}
	}
}
