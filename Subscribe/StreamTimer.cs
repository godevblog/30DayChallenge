using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Subscribe
{
	internal class StreamTimer : IDisposable
	{
		private IObservable<long> _timerObservable;
		private List<IDisposable> _subscribents;

		public StreamTimer(int periodInMilliseconds)
		{
			var dueTime = TimeSpan.FromMilliseconds(0);
			var period = TimeSpan.FromMilliseconds(periodInMilliseconds);
			_subscribents = new List<IDisposable>();

			_timerObservable = Observable.Timer(dueTime, period);
		}

		public void Subscribe(Action<long> onNext)
		{
			var newSubscribent = _timerObservable.Subscribe(
				onNext,
				OnError,
				OnCompleted
				);

			_subscribents.Add(newSubscribent);
		}

		private void OnCompleted()
		{
			Console.WriteLine("Completed");
		}

		private void OnError(Exception ex)
		{
			Console.WriteLine(ex);
		}

		public void Dispose()
		{
			_subscribents.ForEach(x => x.Dispose());
		}
	}
}