using System;
using RXLib.TimeCounter;

namespace DependencyInjection
{
	public class Clock
	{
		private object _obj;

		public Clock(ObservableTimeCounter interval, Object obj)
		{
			_obj = obj;

			interval.Subscribe(time =>
			{
				lock (_obj)
				{
					Console.SetCursorPosition(Console.WindowWidth - 8, 0);

					Console.WriteLine(time);

					Console.Title = time.ToString();
				}
			});
		}
	}
}
