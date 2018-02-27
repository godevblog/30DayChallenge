
using System;
using System.Reactive.Linq;
using RXLib.Extensions;
using RXLib.Factories;

namespace Select
{
	class Program
	{
		static void Main()
		{
			var observableStream = GeneratorFactory.CreateGenerator(0, 255, 1, 50);

			var selectSubscribent = observableStream
				.Select(
					item => new
					{
						Number = item,
						Bits = Convert.ToString(item, 2),
						Hex = Convert.ToString(item, 16).ToUpper()
					}
				).DefaultPrint("Select");

			Console.ReadKey();

			selectSubscribent.Dispose();
		}
	}
}
