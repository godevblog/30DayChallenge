using System.Collections.Generic;
using System.Reactive;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Tests.Interval
{
	public class CreateHotObservableTests
	{
		[Fact]
		public void return_sequence_of_notifications__when__subscribe_to_hot_observable()
		{
			//arrange
			_fixture.SetSimulationDataForHotObservable();

			//act
			var result = act();

			//assert
			_fixture.assert__simulation_notification_of_hot_observable(result);
		}

		private IList<Recorded<Notification<int>>> act()
		{
			return _fixture.act();
		}

		public CreateHotObservableTests()
		{
			_fixture = new CreateObservableTestsFixture();
		}

		private CreateObservableTestsFixture _fixture;
	}
}
