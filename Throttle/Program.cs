using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Throttle
{
	class Program
	{
		static void Main()
		{
			var streamPublisher = new StreamPublisher();

			var throttle = streamPublisher.Stream.Throttle(TimeSpan.FromMilliseconds(500));

			var throttleSubscribe = throttle.Timestamp().Subscribe(
				x => Console.WriteLine("Last item {0}: {1}", x.Value, x.Timestamp),
				Console.WriteLine,
				() => Console.WriteLine("Throttle completed"));

			Console.ReadKey();

			throttleSubscribe.Dispose();
			streamPublisher.Dispose();
		}
	}
}
