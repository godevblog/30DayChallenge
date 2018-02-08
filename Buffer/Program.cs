using System;

namespace Buffer
{
	class Program
	{
		static void Main(string[] args)
		{
			var bufferForCross = new BufferForCross();

			Console.ReadKey();

			bufferForCross.Dispose();
		}
	}
}
