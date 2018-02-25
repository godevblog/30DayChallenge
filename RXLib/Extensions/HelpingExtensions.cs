using System;
using RXLib.ConsoleKey;

namespace RXLib.Extensions
{
	public static class HelpingExtensions
	{
		public static IDisposable BreakWhenKey(this ObservableConsoleKey source, System.ConsoleKey exitKey)
		{
			var subscription = source
				.Subscribe(key =>
				{
					if (key.Key != exitKey)
					{
						return;
					}

					source.Stop();
				});

			return subscription;
		}
	}
}
