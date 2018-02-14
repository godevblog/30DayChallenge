using System;
using System.Collections.Generic;

namespace RXLib.ValentinesDay
{
	public class ObservableValentinesDay : IObservable<ValentinesDayMessage>, IDisposable
	{
		private List<IObserver<ValentinesDayMessage>> _observers;
		private string _time;
		private IDisposable _tickerSubscription;
		private Random _rand;
		private ValentinesDayMessage _valentinesDayMessage;

		private string[] _messageList = {
			"Valentine's Day",
			"Love",
			"Heart",
			" *****   ***** \n" +
			"******* *******\n" +
			" *************\n" +
			"  ***********\n"+
			"   *********\n" +
			"     *****\n" +
			"       *",
			" █████   █████ \n" +
			"███████ ███████\n" +
			" █████████████\n" +
			"  ███████████\n"+
			"   █████████\n" +
			"     █████\n" +
			"       █",

			"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\n" +
			"▒▒█████▒▒▒█████▒▒\n" +
			"▒███████▒███████▒\n" +
			"▒▒█████████████▒▒\n" +
			"▒▒▒███████████▒▒▒\n"+
			"▒▒▒▒█████████▒▒▒▒\n" +
			"▒▒▒▒▒▒█████▒▒▒▒▒▒\n" +
			"▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒"
		};

		public ObservableValentinesDay(IObservable<long> ticker)
		{
			_observers = new List<IObserver<ValentinesDayMessage>>();
			_valentinesDayMessage = new ValentinesDayMessage();
			_rand = new Random();

			SubscribeOnTicker(ticker);
		}

		public IDisposable Subscribe(IObserver<ValentinesDayMessage> observer)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
			}

			return new Unsubscriber<ValentinesDayMessage>(_observers, observer);
		}

		public void Dispose()
		{
			OnComplete();

			_tickerSubscription.Dispose();
		}

		private void SubscribeOnTicker(IObservable<long> ticker)
		{
			_tickerSubscription = ticker.Subscribe(
				index =>
				{
					if (index % 10 != 0) return;

					RandomMessage();
					Publish();
				},
				Console.WriteLine,
				OnComplete
			);
		}

		private void RandomMessage()
		{
			var index = _rand.Next(_messageList.Length);
			_valentinesDayMessage.Message =  _messageList[index];
			_valentinesDayMessage.Color = (ConsoleColor)_rand.Next(1, 16);
		}

		private void Publish()
		{
			_observers.ForEach(observer => observer.OnNext(_valentinesDayMessage));
		}

		private void OnComplete()
		{
			_observers.ForEach(observer => observer.OnCompleted());

			_observers.Clear();
		}
	}
}
