using System;
using System.Linq;
using DataScraper.Model;
using DataScraper.Extension;
using OpenQA.Selenium;

namespace silenium_scaper_temp
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

			IWebElement gameCountry = null;
			IWebElement gameLeague = null;
			IWebElement gameDate  = null;
			GameData gameData = null;

			foreach(var div in contentDivs)
			{
				IWebElement newGameCountry = null;
				IWebElement newGameLeague = null;
				IWebElement newGameDate  = null;

				var divClassAttribute = div.GetAttribute("class");

				if (divClassAttribute.Contains(scraperDataModel.GameConatryAndDate.HeadDivClass))
			    {	
					newGameDate = div.SelectElementFromSetOfSelectors(scraperDataModel.GameConatryAndDate.GameDateSelector);
					
					if (newGameDate == null)
					{
						continue;
					}
					
					gameDate = newGameDate;

					newGameLeague= div.SelectElementFromSetOfSelectors(scraperDataModel.GameConatryAndDate.GameLeagueSelector);
					
					if (newGameLeague != null)
					{
						gameLeague = newGameLeague;
					}
				
					newGameCountry = div.SelectElementFromSetOfSelectors(scraperDataModel.GameConatryAndDate.GameContrySelector);

					if (newGameCountry != null)
					{	
						gameCountry = newGameCountry;
					}

					continue;
				}

				if (divClassAttribute.Contains(scraperDataModel.GameData.RowDivClass))
			    {	
					gameData = new GameData
					{
						GameCountry = gameCountry?.Text,
						GameDate = gameDate?.Text,
						GameLeague = gameLeague?.Text,
						GameTime = div.GetElementBySelector(scraperDataModel.GameData.GameTime)?.Text,
						FirstTeam = div.GetElementBySelector(scraperDataModel.GameData.FirstTeam)?.Text,
						SecondTeam = div.GetElementBySelector(scraperDataModel.GameData.SeconadTeam)?.Text,
						GameScore = div.SelectElementFromSetOfSelectors(scraperDataModel.GameData.GameScore)?.Text
					};
				} 
				else 
				{
					continue; 
				}

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
