
using System;
using System.Reactive.Linq;

namespace RXLib.Extensions
{
	public static class ConvertingExtensions
	{
		public static IObservable<string> ToCharacter(this IObservable<long> source)
		{
			return source.Select(item => char.ConvertFromUtf32((int)item));
		}
	}
}
