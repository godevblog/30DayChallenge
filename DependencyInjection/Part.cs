using System;

namespace DependencyInjection
{
	public class Part
	{
		private static int count = 1;

		private object _obj;
		private string _guid;
		private int _top;

		public Part(IObservable<long> interval, Object obj)
		{
			_obj = obj;
			_guid = Guid.NewGuid().ToString().Substring(0, 5);
			_top = count;
			count++;

			interval.Subscribe(OnNext);
		}

		private void OnNext(long i)
		{
			lock (_obj)
			{
				Console.SetCursorPosition(0, _top);
				Console.Write($"Part: {_guid}\t {i}");
			}
		}
	}
}
