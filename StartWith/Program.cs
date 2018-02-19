
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using RXLib;
using RXLib.TimeCounter;

namespace StartWith
{
	class Program
	{
		static void Main(string[] args)
		{
			var observableProvider = new ObservableProvider();

			var timeList = GetTimeListSamples();

			var subscribent = observableProvider
				.TimeCounter
				.StartWith(timeList)
				.StartWith(
					new Time { Hours = 1, Minutes = 10 },
					new Time { Hours = 2, Minutes = 20 },
					new Time { Hours = 3, Minutes = 10, Seconds = 20 },
					new Time { Hours = 4, Minutes = 10 }
				)
				.Subscribe(
					time =>
					{
						Console.WriteLine(time);
					},
					exception => Console.WriteLine(exception)
				);

			Console.ReadKey();

			subscribent.Dispose();
		}

		private static List<Time> GetTimeListSamples()
		{
			var timeList = new List<Time>();

			for (short i = 10; i < 20; i++)
			{
				timeList.Add(new Time { Hours = i, Minutes = i, Seconds = i });
			}

			return timeList;
		}
	}
}
