namespace DataScraper.Model
{
	public class GameConatryAndDateDescriptor
	{
		public string HeadDivClass => "row-tall";

		public SelectorDescriptor[] GameDateSelector => new[]
		{
			new SelectorDescriptor
			{
				Selector = "div > div.right.fs11",
				Order = 0	 
			}
		};

		public SelectorDescriptor[] GameContrySelector  => new[]
		{
			new SelectorDescriptor
			{
				Selector = "div > div.left > a > strong",
				Order = 0	 
			},
			new SelectorDescriptor
			{
				Selector = "div > div.left > strong",
				Order = 1	 
			}
		};

		public SelectorDescriptor[] GameLeagueSelector  => new[]
		{
			new SelectorDescriptor
			{
				Selector = "div > div.left",
				Order = 0	 
			},
			new SelectorDescriptor
			{
				Selector = "div > div.left > a",
				Order = 1	 
			}
		};
	}
}
