using System;

namespace Timer
{
	class Program
	{
		static void Main(string[] args)
		{
			var dueTime = TimeSpan.FromSeconds(5);
			var period = TimeSpan.FromSeconds(1);

			var timerDueTime = new TimerDueTime(dueTime);
			var timerDueTimeAndPeriod = new TimerDueTimeAndPeriod(dueTime, period);

			Console.ReadKey();

			timerDueTime.Dispose();
			timerDueTimeAndPeriod.Dispose();
		}
	}
}
