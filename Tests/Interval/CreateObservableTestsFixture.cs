using System;
using System.Collections.Generic;
using System.Reactive;
using Microsoft.Reactive.Testing;
using Shouldly;
using Tests.Extensions;

namespace Tests.Interval
{
	internal class CreateObservableTestsFixture
	{
		#region arrange
		private TestScheduler _testScheduler;
		private IObservable<int> _sourceObservableInterval;
		private int _disposed;

		public CreateObservableTestsFixture()
		{
			_testScheduler = new TestScheduler();
		}

		public void SetSimulationDataForColdObservable()
		{
			var second = 1;
			var value = 0;

			_sourceObservableInterval = _testScheduler.CreateColdObservable(
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnError<int>(new Exception())),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnError<int>(new ArgumentException())),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnCompleted<int>())
			);

			_disposed = second;
		}

		public void SetSimulationDataForHotObservable()
		{
			var second = 1;
			var value = 0;

			_sourceObservableInterval = _testScheduler.CreateHotObservable(
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnError<int>(new Exception())),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnError<int>(new ArgumentException())),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnNext(value++)),
				new Recorded<Notification<int>>(TimeSpan.FromSeconds(second++).Ticks, Notification.CreateOnCompleted<int>())
			);

			_disposed = second;
		}
		#endregion arrange

		#region act
		public IList<Recorded<Notification<int>>> act()
		{
			var notificationRecorded = _testScheduler.Start(() => _sourceObservableInterval,
				0,
				0,
				TimeSpan.FromSeconds(_disposed).Ticks);

			return notificationRecorded.Messages;
		}
		#endregion act

		#region assert
		public void assert__simulation_notification_of_cold_observable(IList<Recorded<Notification<int>>> messages)
		{
			assert_for_messages_time_cold(messages);

			assert__for_messages_count(messages);
			assert__for_test_scheduler_clock();
			assert__for_messages(messages);
		}

		public void assert__simulation_notification_of_hot_observable(IList<Recorded<Notification<int>>> messages)
		{
			assert__for_messages_time_hot(messages);

			assert__for_messages_count(messages);
			assert__for_test_scheduler_clock();
			assert__for_messages(messages);
		}

		private void assert__for_messages_count(IList<Recorded<Notification<int>>> messages)
		{
			var shouldBeValue = _disposed - 1;

			messages.Count.ShouldBe(shouldBeValue);
		}

		private void assert__for_test_scheduler_clock()
		{
			_testScheduler.Clock.ShouldBe(_disposed * TimeSpan.FromSeconds(1).Ticks);
		}

		private void assert__for_messages_time_hot(IList<Recorded<Notification<int>>> messages)
		{
			for (var i = 0; i < _disposed - 1; i++)
			{
				messages[i].Time.ShouldBe(TimeSpan.FromSeconds(i + 1).Ticks);
			}
		}

		private void assert_for_messages_time_cold(IList<Recorded<Notification<int>>> messages)
		{
			for (var i = 0; i < _disposed - 1; i++)
			{
				messages[i].Time.ShouldBe(TimeSpan.FromSeconds(i + 1).Ticks + 1);
			}
		}

		private void assert__for_messages(IList<Recorded<Notification<int>>> messages)
		{
			var index = 0;
			var value = 0;
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnError(typeof(Exception));
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnError(typeof(ArgumentException));
			messages[index++].Value.ShouldBeOnNext(value++);
			messages[index++].Value.ShouldBeOnNext(value++);
		}
		#endregion assert
	}
}