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
			
			IWebElement gameTypeCountry = null;
			IWebElement gameTypeLeague = null;
			IWebElement gameDate  = null;

			foreach(var div in contentDivs)
			{
				IWebElement newGameTypeCountry = null;
				IWebElement newGameTypeLeague = null;
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

					newGameTypeLeague= div.SelectElementFromSetOfSelectors(scraperDataModel.GameConatryAndDate.GameLeagueSelector);
					
					if (newGameTypeLeague != null)
					{
						gameTypeLeague = newGameTypeLeague;
					}
				
					newGameTypeCountry = div.SelectElementFromSetOfSelectors(scraperDataModel.GameConatryAndDate.GameContrySelector);

					if (newGameTypeCountry != null)
					{	
						gameTypeCountry = newGameTypeCountry;
					}

					continue;
				}

				IWebElement gameTime = null;
				IWebElement firstTeam = null;
				IWebElement secondTeam = null;
				IWebElement gameScore = null;
				
				if (divClassAttribute.Contains(scraperDataModel.GameData.RowDivClass))
			    {	
					gameTime = div.GetElementBySelector(scraperDataModel.GameData.GameTime);
					firstTeam = div.GetElementBySelector(scraperDataModel.GameData.FirstTeam);
					secondTeam = div.GetElementBySelector(scraperDataModel.GameData.SeconadTeam);
					gameScore = div.SelectElementFromSetOfSelectors(scraperDataModel.GameData.GameScore);
				}

		    	Console.WriteLine("Game Country: " + gameTypeCountry?.Text);	
		    	Console.WriteLine("Game League: " + gameTypeLeague?.Text);	
		    	Console.WriteLine("Game Date: " + gameDate?.Text);	
				Console.WriteLine("Game Time: " + gameTime?.Text);
				Console.WriteLine("Game First Team: " + firstTeam?.Text);
				Console.WriteLine("Game Second Team: " + secondTeam?.Text);
				Console.WriteLine("Game Score: "+ gameScore?.Text);
				Console.WriteLine();
			}

			driver.Quit();
        }
	}
}
