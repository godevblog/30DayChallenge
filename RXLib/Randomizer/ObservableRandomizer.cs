using System;
using System.Collections.Generic;
using System.Threading;

namespace RXLib.Randomizer
{
	public class ObservableRandomizer : IObservable<Randomed>, IDisposable
	{
		private Random _random;
		private List<IObserver<Randomed>> _observers;
		private Randomed _randomed;
		private bool _threadInProgress;
		private Thread _thread;
		private int _from;
		private int _to;
		private Random _random2;

		protected ObservableRandomizer(int from, int to)
		{
			_from = from;
			_to = to;
			_randomed = new Randomed();

			_observers = new List<IObserver<Randomed>>();

			StartThread();
		}

		public IDisposable Subscribe(IObserver<Randomed> observer)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
			}

			return new Unsubscriber<Randomed>(_observers, observer);
		}

		private void StartThread()
		{
			_threadInProgress = true;

			_thread = new Thread(ThreadMethod);
			_thread.Start();
		}

		private void ThreadMethod()
		{
			while (_threadInProgress)
			{
				var delay = new Random(DateTime.Now.Millisecond).Next(200, 1000);
				Thread.Sleep(delay);

				_randomed.Ticks = DateTime.Now.Ticks;
				_randomed.Value = new Random(DateTime.Now.Millisecond).Next(_from, _to);

				Publish();
			}
		}

		public void Dispose()
		{
			OnComplete();
		}

		private void Publish()
		{
			_observers.ForEach(observer => observer.OnNext(_randomed));
		}

		private void OnComplete()
		{
			_threadInProgress = false;

			_thread?.Abort();

			_observers.ForEach(observer => observer.OnCompleted());

			_observers.Clear();
		}

		public static ObservableRandomizer Create(int from, int to)
			=> new ObservableRandomizer(from, to);
	}
}
