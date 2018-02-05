using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Shedulers
{
	public class ExampleTimer : IDisposable
	{
		private IObservable<long> _timerObservable;
		private IDisposable _subscribe;
		private string _name;
		private TimeSpan _dueTime;
		private TimeSpan _period;
		
		public ExampleTimer(String name, IScheduler scheduler)
		{
			Initialize(name);

			_timerObservable = Observable.Timer(_dueTime, _period, scheduler);

			Subscribe();
		}

		private void Initialize(String name)
		{
			_dueTime = TimeSpan.FromSeconds(1);
			_period = TimeSpan.FromSeconds(1);
			_name = name;
		}

		private void Subscribe()
		{
			_subscribe = _timerObservable.Subscribe(index =>
			{
				Console.WriteLine($"[{_name}] : {index}");
			});
		}

		public void Dispose()
		{
			_subscribe.Dispose();
		}
	}
}
