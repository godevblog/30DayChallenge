using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delay
{
	class Program
	{
		static void Main(string[] args)
		{
			var rand = new Random();
			var spies = new List<IDisposable>();
			var observableInterval = Observable.Interval(TimeSpan.FromMilliseconds(5000));

			var subscribeInterval = observableInterval.Subscribe(index =>
			{
				Console.WriteLine($"{DateTime.UtcNow} Followed!");
			});
			
			for (var i = 0; i < 10; i++)
			{
				spies.Add(new Spy(observableInterval, rand.Next(3000), $"Spy {i}"));
			}

			Console.ReadKey();

			subscribeInterval.Dispose();
			spies.ForEach(spy => spy.Dispose());
		}
	}
}
