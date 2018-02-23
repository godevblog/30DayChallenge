using System;
using System.Reactive.Linq;
using RXLib.Extensions;

namespace Zip
{
	class Program
	{
		static void Main(string[] args)
		{
			var observableIntervalForNumbers = Observable.Interval(TimeSpan.FromMilliseconds(10))
				.Take(256);

			var observableIntervalForChars = Observable.Interval(TimeSpan.FromMilliseconds(50))
				.Take(256)
				.ToCharacter();

			var subscription1 = observableIntervalForNumbers
				.Zip(observableIntervalForChars, (leftItem, rightItem) => new
				{
					Index = leftItem,
					Character = rightItem
				})
				.DefaultPrint("Zip test");

			Console.ReadKey();

			subscription1.Dispose();
		}
	}
}
