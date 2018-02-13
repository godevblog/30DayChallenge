using System;

namespace Generators
{
	class Program
	{
		static void Main(string[] args)
		{
			var rangeGenerator = new RangeGenerator(-100, 200);

			Console.WriteLine();
		
			var generator = new FactorialGenerator();

			Console.ReadKey();

			generator.Dispose();
			rangeGenerator.Dispose();
		}
	}
}
