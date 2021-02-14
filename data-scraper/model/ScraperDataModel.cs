namespace DataScraper.Model
{
	public class ScraperDataModel
	{
		public string Url => @"https://www.livescores.com/";

		public string ContentSelector => "body > div.wrapper > div.content > div";

		public GameDataDescriptor GameData => new GameDataDescriptor();

		public GameConatryAndDateDescriptor GameConatryAndDate => new GameConatryAndDateDescriptor();
	}
}
