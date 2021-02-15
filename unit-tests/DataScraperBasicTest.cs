using Xunit;
using DataScraper.Model;
using DataScraper.Scraper;

namespace DataScraper.UnitTests
{
    public class Test_PageScraper
    {
		[Fact]	
		public void Test_AtLeastOne_GameScraped()
		{
			/*var scraperDataModel = new ScraperDataModel();
			
			var driver = scraperDataModel.Url.GetChromeDriver();
					
			driver.Title

			var contentDivs = driver.FindElements(By.CssSelector(scraperDataModel.ContentSelector));

			var gameData = new GameData();
			var pageScraper = new PageScraper(new IScraper[]
							{
								new HeaderDataScraper(scraperDataModel.GameConatryAndDate),
								new GameRowScraper(scraperDataModel.GameData)
							});
			
			foreach(var div in contentDivs)
			{
				pageScraper.ScrapeData(div, gameData);	

		    	gameData.GameCountry
		    	gameData.GameLeague
		    	gameData.GameDate
				gameData.GameTime
				gameData.FirstTeam
				gameData.SecondTeam
				gameData.GameScore
			}*/
		}	
    }
}
