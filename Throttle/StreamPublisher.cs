using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading;

namespace Throttle
{
	internal class StreamPublisher : IDisposable
	{
		private bool _end;
		private bool _publish;
		private IDisposable _switcherSubscribe;

		public IObservable<int> Stream => GetStream().ToObservable();

		public StreamPublisher()
		{
			_publish = true;
			InitializeSwitcher();
		}

		private IEnumerable<int> GetStream()
		{
			var i = 0;

			while (!_end)
			{
				while (!_publish)
				{
					Thread.Sleep(100);
				}

				Console.Write($"{i},");

				yield return i++;

				Thread.Sleep(100);
			}
		}

		private void InitializeSwitcher()
		{
			var switcherTimeSpan = TimeSpan.FromSeconds(5);
			var switcher = Observable.Interval(switcherTimeSpan);
			_switcherSubscribe = switcher.Subscribe(x =>
				{
					_publish = !_publish;

					var swithcTo = _publish ? "publish (unlock stream)" : "not publish (lock stream)";

					Console.WriteLine();
					Console.WriteLine($"Switch to {swithcTo}");
					Console.WriteLine();
				},
				Console.WriteLine,
				() => { Console.WriteLine("Switcher completed"); }
			);
		}

		public void Dispose()
		{
			_end = true;
			_switcherSubscribe.Dispose();
		}
	}
}