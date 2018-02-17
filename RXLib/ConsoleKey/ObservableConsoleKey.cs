using System;
using System.Collections.Generic;
using RXLib.TimeCounter;

namespace RXLib.ConsoleKey
{
	public class ObservableConsoleKey : IObservable<ConsoleKeyInfo>, IDisposable
	{
		private bool _catchKeys;
		private List<IObserver<ConsoleKeyInfo>> _observers;

		public ObservableConsoleKey()
		{
			_observers = new List<IObserver<ConsoleKeyInfo>>();
		}

		public void Start()
		{
			_catchKeys = true;

			while (_catchKeys)
			{
				var currentKey = Console.ReadKey(true);
				Publish(currentKey);
			}
		}

		public void Stop()
		{
			_catchKeys = false;
		}

		public IDisposable Subscribe(IObserver<ConsoleKeyInfo> observer)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
			}

			return new Unsubscriber<ConsoleKeyInfo>(_observers, observer);
		}

		public void Dispose()
		{
			OnComplete();
		}

		private void Publish(ConsoleKeyInfo currentKey)
		{
			_observers.ForEach(observer => observer.OnNext(currentKey));
		}

		private void OnComplete()
		{
			_observers.ForEach(observer => observer.OnCompleted());

			_observers.Clear();

			_catchKeys = false;
		}
	}
}
