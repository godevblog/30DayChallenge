using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
	class Program
	{
		static void Main(string[] args)
		{
			var observable = Observable.Interval(TimeSpan.FromMilliseconds(10));

			var sampleList = new List<IDisposable>
			{
				new PickASample(observable, 1),
				new PickASample(observable, 3),
				new PickASample(observable, 5)
			};

			Console.ReadKey();

			sampleList.ForEach(s => s.Dispose());
		}
	}
}
