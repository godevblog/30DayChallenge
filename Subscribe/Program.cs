using System;

namespace Subscribe
{
	class Program
	{
		static void Main(string[] args)
		{
			var timer = new StreamTimer(100);

			timer.Subscribe(index =>
			{
				Console.WriteLine($"YEAH {index}");
			});

			timer.Subscribe(index =>
			{
				try
				{
					throw new Exception("TRAH!");
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			});

			//To crash app, uncomment code below :)
			/*timer.Subscribe(index =>
			{
				throw new Exception("TRAH!");
			});*/

			Console.ReadKey();

			timer.Dispose();
		}
	}
}
