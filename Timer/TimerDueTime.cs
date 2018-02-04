using System;
using System.Reactive.Linq;

namespace Timer
{
	public class TimerDueTime: IDisposable
	{
		private IObservable<long> _timerObservable;
		private IDisposable _lambdaSubscribe;
		private IDisposable _methodSubscribe;
		private string _className;

		public TimerDueTime(TimeSpan dueTime)
		{
			_className = this.GetType().Name;

			_timerObservable = Observable.Timer(dueTime);

			_lambdaSubscribe = _timerObservable.Subscribe(index =>
			{
				Console.WriteLine($"[{_className}] : From lambda: {index}");
			});

			_methodSubscribe = _timerObservable.Subscribe(OnTimer);
		}

		private void OnTimer(long index)
		{
			Console.WriteLine($"[{_className}] : From method: {index}");
		}

		public void Dispose()
		{
			_lambdaSubscribe.Dispose();
			_methodSubscribe.Dispose();
		}
	}
}
