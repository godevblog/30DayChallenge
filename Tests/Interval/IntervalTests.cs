using System.Collections.Generic;
using Xunit;

namespace Tests.Interval
{
	public class IntervalTests
	{
		[Theory]
		[InlineData(0)]
		[InlineData(10)]
		[InlineData(100)]
		[InlineData(1000)]
		[InlineData(100000)]
		public void return_sequence_of_numbers__when__subscribe_to_interval_observable_stream(int count)
		{
			//arrange
			_fixture.PrepareStream(count);

			//act
			var result = act();

			//assert
			_fixture.assert__sequence_of_numbers(result);
		}

		private ICollection<long> act()
		{
			return _fixture.act();
		}

		public IntervalTests()
		{
			_fixture = new IntervalTestsFixture();
		}

		private IntervalTestsFixture _fixture;
	}
}
