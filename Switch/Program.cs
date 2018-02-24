
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using RXLib.Extensions;
using RXLib.Randomizer;

namespace Switch
{
	class Program
	{
		static void Main()
		{
			var randomedList = new List<IObservable<Randomed>>();
			randomedList.Add(ObservableRandomizer.Create(0, 100));
			randomedList.Add(ObservableRandomizer.Create(1000, 2000));
			randomedList.Add(ObservableRandomizer.Create(0, 100));

			for (var i = 0; i < randomedList.Count; i++)
			{
				randomedList[i].DefaultPrint($"Randomizer{i}");
			}
			
			var observableRandomedList = randomedList.ToObservable();

			var observableSequence = observableRandomedList.Switch();

			var subscription1 = observableSequence.DefaultPrint("Switch");

			Console.ReadKey();

			subscription1.Dispose();
		}
	}
}
