using System.Collections.Generic;
using System.Reactive;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Tests.Interval
{
	public class CreateColdObservableTests
	{
		[Fact]
		public void return_sequence_of_notifications__when__subscribe_to_cold_observable()
		{
			//arrange
			_fixture.SetSimulationDataForColdObservable();

			//act
			var result = act();

			//assert
			_fixture.assert__simulation_notification_of_cold_observable(result);
		}

		private IList<Recorded<Notification<int>>> act()
		{
			return _fixture.act();
		}

		public CreateColdObservableTests()
		{
			_fixture = new CreateObservableTestsFixture();
		}

		private CreateObservableTestsFixture _fixture;
	}
}
