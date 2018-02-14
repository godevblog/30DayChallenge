using System;
using System.Collections.Generic;

namespace RXLib
{
	public class Unsubscriber<T> : IDisposable
	{
		private List<IObserver<T>> _observers;
		private IObserver<T> _observer;

		public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
		{
			_observers = observers;
			_observer = observer;
		}

		public void Dispose()
		{
			if (_observer != null && _observers.Contains(_observer))
			{
				_observers.Remove(_observer);
			}
		}
	}
}