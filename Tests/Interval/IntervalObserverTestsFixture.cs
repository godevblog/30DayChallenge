using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Shouldly;

namespace Tests.Interval
{
	internal class IntervalObserverTestsFixture
	{
		#region arrange
		private TestScheduler _testScheduler;
		private IObservable<long> _sourceObservableInterval;
		private int _disposed;
		private int _created;
		private int _subscribed;

		public IntervalObserverTestsFixture()
		{
			_testScheduler = new TestScheduler();
		}

		public void SetSimulationData(int created, int subscribed, int disposed)
		{
			_disposed = disposed;
			_created = created;
			_subscribed = subscribed;

			var period = TimeSpan.FromSeconds(1);

			_sourceObservableInterval = Observable.Interval(period, _testScheduler)
			.Take(_disposed);
		}
		#endregion arrange

		#region act
		public IList<Recorded<Notification<long>>> act()
		{
			var notificationRecorded = _testScheduler.Start(() => _sourceObservableInterval,
				TimeSpan.FromSeconds(_created).Ticks,
				TimeSpan.FromSeconds(_subscribed).Ticks,
				TimeSpan.FromSeconds(_disposed).Ticks);

			return notificationRecorded.Messages;
		}
		#endregion act

		#region assert__simulation_notification
		public void assert__simulation_notification(IList<Recorded<Notification<long>>> result)
		{
			var shouldBeValue = _disposed - 1;
			shouldBeValue -= _subscribed;

			result.Count.ShouldBe(shouldBeValue);

			long index = 0;
			foreach (var item in result)
			{
				item.Value.Value.ShouldBe(index++);
			}
		}
		#endregion assert__simulation_notification
	}
}