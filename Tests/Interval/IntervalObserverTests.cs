using System.Collections.Generic;
using System.Reactive;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Tests.Interval
{
	public class IntervalObserverTests
	{
		[Theory]
		[InlineData(0, 0, 4)]
		[InlineData(0, 0, 10)]
		[InlineData(0, 2, 10)]
		[InlineData(2, 2, 20)]
		[InlineData(2, 5, 20)]
		public void return_sequence_of_notifications__when__subscribe_to_interval_distributor(int created, int subscribed, int disposed)
		{
			//arrange
			_fixture.SetSimulationData(created, subscribed, disposed);

			//act
			var result = act();

			//assert
			_fixture.assert__simulation_notification(result);
		}

		private IList<Recorded<Notification<long>>> act()
		{
			return _fixture.act();
		}

		public IntervalObserverTests()
		{
			_fixture = new IntervalObserverTestsFixture();
		}

		private IntervalObserverTestsFixture _fixture;
	}
}
