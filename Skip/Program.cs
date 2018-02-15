using System;
using System.Reactive.Linq;
using RXLib;

namespace Skip
{
	class Program
	{
		private const int GenerateItemCount = 20;
		private static ObservableProvider _observableProvider;
		private static IObservable<int> _generator;
		private static IDisposable _skipLastSubscribent;

		static void Main(string[] args)
		{
			InitializeGenerators();

			AddSkipLastSubscribent();

			AddSkipSubscribent();

			AddSkipWhileSubscribent();

			AddSkipUntilSubscribent();

			AddAllItemSubscribent();

			Console.ReadKey();

			_observableProvider.Dispose();
			_skipLastSubscribent.Dispose();
		}

		private static void InitializeGenerators()
		{
			_observableProvider = new ObservableProvider();

			var initialState = 0;

			_generator = Observable.Generate(
				initialState,
				condition => condition < GenerateItemCount,
				iterate => iterate + 1,
				resultSelector => resultSelector,
				timeSelector => TimeSpan.FromMilliseconds(100)
			);
		}

		private static void AddSkipLastSubscribent()
		{
			_skipLastSubscribent = _generator
				.SkipLast(10)
				.Subscribe(
					item =>
					{
						Console.WriteLine($"SkipeLast 10 items from {GenerateItemCount}: {item}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddSkipWhileSubscribent()
		{
			_observableProvider.TimeCounter
				.SkipWhile(x => x.Minutes == 0)
				.Subscribe(
					time =>
					{
						Console.WriteLine($"SkipWhile, ignore first minute: {time}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddSkipSubscribent()
		{
			_observableProvider.TimeCounter
				.Skip(10)
				.Subscribe(
					time =>
					{
						Console.WriteLine($"Skip (10): {time}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddAllItemSubscribent()
		{
			_observableProvider.TimeCounter
				.Subscribe(
					time =>
					{
						Console.WriteLine($"Time: {time}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}

		private static void AddSkipUntilSubscribent()
		{
			_observableProvider.TimeCounter
				.SkipUntil(_generator)
				.Subscribe(
					time =>
					{
						Console.WriteLine($"SkipUntil, ignore: {time}");
					},
					ex =>
					{
						Console.WriteLine(ex);
					});
		}
	}
}
