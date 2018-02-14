namespace RXLib.TimeCounter
{
	public class Time
	{
		public Time()
		{
			Seconds = 0;
			Minutes = 0;
			Hours = 0;
		}

		public short Seconds { get; set; }
		public short Minutes { get; set; }
		public long Hours { get; set; }

		public override string ToString()
		{
			return $"{Hours}:{Minutes:00}:{Seconds:00}";
		}
	}
}