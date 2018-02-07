using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interval
{
	class Program
	{
		static void Main(string[] args)
		{
			var subscribents = new List<IDisposable>();

			var observableInterval = Observable.Interval(
				TimeSpan.FromMilliseconds(200));

			var observableIntervalSheduler = Observable.Interval(
				TimeSpan.FromMilliseconds(2000), 
				TaskPoolScheduler.Default);

			AddSubscribent(subscribents, observableInterval);
			AddSubscribent(subscribents, observableIntervalSheduler);

			Console.ReadKey();

			DisposeAllSubscribents(subscribents);
		}

		private static void DisposeAllSubscribents(List<IDisposable> subscribents)
		{
			subscribents.ForEach(subscribent => subscribent.Dispose());
		}

		private static void AddSubscribent(List<IDisposable> subscribents, IObservable<long> observable)
		{
			var subscribent = observable
				.Subscribe(
					index => { Console.WriteLine($"Index: {index}"); },
					exception => { Console.WriteLine($"Exception: {exception}"); },
					() => { Console.WriteLine("Completed"); });

			subscribents.Add(subscribent);
		}
	}
}
