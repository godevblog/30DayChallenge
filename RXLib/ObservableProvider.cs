using System;
using System.Reactive.Linq;
using RXLib.ConsoleKey;
using RXLib.Randomizer;
using RXLib.TimeCounter;
using RXLib.ValentinesDay;

namespace RXLib
{
	public class ObservableProvider : IDisposable
	{
		public ObservableTimeCounter TimeCounter { get; private set; }
		public ObservableValentinesDay ValentinesDay { get; private set; }
		public ObservableConsoleKey ConsoleKey { get; private set; }
		public ObservableRandomizer Random { get; private set; }
		
		private IObservable<long> _ticker;
		
		public ObservableProvider()
		{
			InitializeTicker();

			Initialize();
		}

		private void Initialize()
		{
			TimeCounter = new ObservableTimeCounter(_ticker);
			ValentinesDay = new ObservableValentinesDay(_ticker);
			ConsoleKey = new ObservableConsoleKey();
			Random = ObservableRandomizer.Create(0, 100);
		}

		private void InitializeTicker()
		{
			var period = TimeSpan.FromMilliseconds(100);
			_ticker = Observable.Interval(period);
		}

		public void Dispose()
		{
			ValentinesDay.Dispose();
			TimeCounter.Dispose();
			ConsoleKey.Dispose();
			Random.Dispose();
		}
	}
}
