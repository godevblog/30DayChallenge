using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;

namespace Buffer
{
	public class BufferForCross : IDisposable
	{
		private IDisposable _bufferSubscribent;
		private IDisposable _intervalSubscribent;
		private IObservable<long> _observableCarList;
		private IObservable<IList<long>> _bufferObservable;

		public BufferForCross()
		{
			InitalizeCarGenerator();
			InitializeCarBuffer();

			SubscribeToBuffer();
		}

		private void SubscribeToBuffer()
		{
			_bufferSubscribent = _bufferObservable.Subscribe(
				newCars =>
				{
					try
					{
						Console.WriteLine("Green light!");

						foreach (var carId in newCars)
						{
							Console.WriteLine($"\t\tPass car {carId}.");
							Thread.Sleep(100);
						}

						Console.WriteLine("Red light!");
						Console.WriteLine();
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				},
				() => Console.WriteLine("Completed"));
		}

		private void InitializeCarBuffer()
		{
			var wait = TimeSpan.FromSeconds(2);
			var period = TimeSpan.FromSeconds(5);

			_bufferObservable = Observable.Buffer(_observableCarList, wait, period);
		}

		private void InitalizeCarGenerator()
		{
			_observableCarList = Observable.Interval(TimeSpan.FromMilliseconds(500));

			_intervalSubscribent = _observableCarList.Subscribe(
				carId =>
				{
					Console.WriteLine($"New Car {carId} ");
				});
		}

		public void Dispose()
		{
			_intervalSubscribent.Dispose();
			_bufferSubscribent.Dispose();
		}
	}
}
