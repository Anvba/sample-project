namespace DataScraper.Model
{
	public class ScraperDataModel
	{
		public string Url => @"https://www.livescores.com/";

		//@"https://www.livescores.com/soccer/spain/"
		//@"https://www.livescores.com/soccer/belgium/u21-pro-league-group-1/"

		public string ContentSelector => "body > div.wrapper > div.content > div";

		public GameDataDescriptor GameData => new GameDataDescriptor();

		public GameConatryAndDateDescriptor GameConatryAndDate => new GameConatryAndDateDescriptor();
	}
}
