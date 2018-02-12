using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;

namespace Schedulers
{
	class Program
	{
		static void Main(string[] args)
		{
			var timerList = new List<ExampleTimer>
			{
				//asynchronous
				new ExampleTimer("NewThreadScheduler", NewThreadScheduler.Default),
				new ExampleTimer("TaskPoolScheduler", TaskPoolScheduler.Default),
				new ExampleTimer("ThreadPoolScheduler", ThreadPoolScheduler.Instance),

				//synchronous, only one can work at the same time
				new ExampleTimer("CurrentThreadScheduler", CurrentThreadScheduler.Instance),
				new ExampleTimer("ImmediateScheduler", ImmediateScheduler.Instance),
			};

			Console.ReadKey();

			timerList.ForEach(x => x.Dispose());
		}
	}
}
