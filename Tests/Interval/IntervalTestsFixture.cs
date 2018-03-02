using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Shouldly;

namespace Tests.Interval
{
	public class IntervalTestsFixture
	{
		#region arrange
		private IObservable<long> _observableInterval;
		private TestScheduler _testScheduler;
		private IList<long> _expectedSequence;
		
		public void PrepareStream(int count)
		{
			InitializeStream(count);

			InitializeExpectedValues(count);
		}

		private void InitializeStream(int count)
		{
			_testScheduler = new TestScheduler();
			var period = TimeSpan.FromSeconds(1);

			_observableInterval = Observable
				.Interval(period, _testScheduler)
				.Take(count);
		}

		private void InitializeExpectedValues(int count)
		{
			_expectedSequence = new List<long>();

			for (var i = 0; i < count; i++)
			{
				_expectedSequence.Add(i);
			}
		}
		#endregion arrange

		#region act
		public ICollection<long> act()
		{
			var actualValues = new List<long>();

			_observableInterval.Subscribe(actualValues.Add);

			_testScheduler.Start();

			return actualValues;
		}
		#endregion act

		#region asserts
		public void assert__sequence_of_numbers(ICollection<long> result)
		{
			_expectedSequence.ShouldBe(result);
		}
		#endregion asserts
	}
}
