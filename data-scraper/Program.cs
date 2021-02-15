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

				gameData = new GameData
				{
					GameContry = gameCountry?.Text,
					GameDate = gameDate?.Text,
					GameLeague = gameLeague?.Text,
					GameTime = gameTime?.Text,
					FirstTeam = firstTeam?.Text,
					SeconadTeam = secondTeam?.Text,
					GameScore = gameScore?.Text
				};

		    	Console.WriteLine("Game Country: " + gameCountry?.Text);	
		    	Console.WriteLine("Game League: " + gameLeague?.Text);	
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
