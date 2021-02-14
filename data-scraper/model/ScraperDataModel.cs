namespace DataScraper.Model
{
	public class ScraperDataModel
	{
		public string Url => @"https://www.livescores.com/";

		public string ContentSelector => "body > div.wrapper > div.content > div";

		public string HeadDivClass => "row-tall";

		public string RowDivClass => "row-gray";

		public string GameTime => "div.min";

		public string FirstTeam => "div.tright";

		public string SeconadTeam => "div:nth-child(4)";

		public SelectorDescriptor[]  GameScore  => new[]
		{
			new SelectorDescriptor
			{
				Selector = "div.sco > a.scorelink",
				Order = 0	 
			},
			new SelectorDescriptor
			{
				Selector = "div.sco",
				Order = 1	 
			}
		};

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
