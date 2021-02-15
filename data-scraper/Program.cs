using System;
using DataScraper.Model;
using DataScraper.Extension;
using DataScraper.Scraper;
using OpenQA.Selenium;

namespace DataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting test console app");
			
			var scraperDataModel = new ScraperDataModel();
			
			var driver = scraperDataModel.Url.GetChromeDriver();
					
			Console.WriteLine("Page titile " + driver.Title);
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

		    	Console.WriteLine("Game Country: " + gameData.GameCountry);	
		    	Console.WriteLine("Game League: " + gameData.GameLeague);	
		    	Console.WriteLine("Game Date: " + gameData.GameDate);	
				Console.WriteLine("Game Time: " + gameData.GameTime);
				Console.WriteLine("Game First Team: " + gameData.FirstTeam);
				Console.WriteLine("Game Second Team: " + gameData.SecondTeam);
				Console.WriteLine("Game Score: "+ gameData.GameScore);
				Console.WriteLine();
			}

			driver.Quit();
        }
	}
}
