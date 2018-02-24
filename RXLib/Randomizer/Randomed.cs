namespace RXLib.Randomizer
{
	public class Randomed
	{
		public int Value { get; set; }
		public long Ticks { get; set; }

		public Randomed()
		{
			Value = 0;
			Ticks = 0;
		}

		public override string ToString()
		{
			return $"{Ticks}->{Value}";
		}
	}
}
