using System;
using System.Collections.Generic;

namespace RXLib.TimeCounter
{
	public class ObservableTimeCounter : IObservable<Time>, IDisposable
	{
		private const int MinutesLimit = 59;
		private const int HourLimit = 59;

		private List<IObserver<Time>> _observers;
		private Time _time;
		private IDisposable _tickerSubscription;

		public ObservableTimeCounter(IObservable<long> ticker)
		{
			_observers = new List<IObserver<Time>>();
			_time = new Time();

			SubscribeOnTicker(ticker);
		}

		public IDisposable Subscribe(IObserver<Time> observer)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
			}

			return new Unsubscriber<Time>(_observers, observer);
		}

		public void Dispose()
		{
			OnComplete();

			_tickerSubscription.Dispose();
		}

		private void SubscribeOnTicker(IObservable<long> ticker)
		{
			_tickerSubscription = ticker.Subscribe(
				index =>
				{
					if (index % 10 != 0) return;

					CalculateTime();
				},
				Console.WriteLine,
				OnComplete
			);
		}

		private void CalculateTime()
		{
			_time.Seconds++;

			if (_time.Seconds > MinutesLimit)
			{
				_time.Seconds = 0;
				_time.Minutes++;
			}

			if (_time.Minutes > HourLimit)
			{
				_time.Minutes = 0;
				_time.Hours++;
			}

			Publish();
		}

		private void Publish()
		{
			_observers.ForEach(observer => observer.OnNext(_time));
		}

		private void OnComplete()
		{
			_observers.ForEach(observer => observer.OnCompleted());

			_observers.Clear();
		}
	}
}
