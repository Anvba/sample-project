namespace DataScraper.Model
{
	public class GameDataDescriptor
	{
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
			
	}
}
