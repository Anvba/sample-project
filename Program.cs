using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace silenium_scaper_temp
{
    class Program
    {
		class SelectorDescriptor
		{
			public string Selector { get; set; }
	   		
			public int Order { get; set; }		
		}

		class ScraperDataModel
		{
			public string Url => @"https://www.livescores.com/";

			public string ContentSelector => "body > div.wrapper > div.content > div";

			public string HeadDivClass => "row-tall";

			public string RowDivClass => "row-gray";

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
		
        static void Main(string[] args)
        {
            Console.WriteLine("Starting test console app");
			
			var scraperDataModel = new ScraperDataModel();
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddArguments("headless");
			chromeOptions.AddArgument("--disable-javascript");

			var driver = new ChromeDriver(chromeOptions);
			driver.Navigate().GoToUrl(scraperDataModel.Url);
			
			//driver.Navigate().GoToUrl(@"https://www.livescores.com/soccer/spain/");
			//driver.Navigate().GoToUrl(@"https://www.livescores.com/soccer/belgium/u21-pro-league-group-1/");

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

				if (divClassAttribute.Contains(scraperDataModel.HeadDivClass))
			    {	
					newGameDate = SelectElementFromSetOfSelectors(div, scraperDataModel.GameDateSelector);
					
					if (newGameDate == null)
					{
						continue;
					}
					
					gameDate = newGameDate;

					newGameTypeLeague= SelectElementFromSetOfSelectors(div, scraperDataModel.GameLeagueSelector);
					
					if (newGameTypeLeague != null)
					{
						gameTypeLeague = newGameTypeLeague;
					}
				
					newGameTypeCountry	= SelectElementFromSetOfSelectors(div, scraperDataModel.GameContrySelector);

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
				
				if (divClassAttribute.Contains(scraperDataModel.RowDivClass))
			    {	
					gameTime = GetElementBySelector(div, "div.min");
					firstTeam = GetElementBySelector(div, "div.tright");
					secondTeam = GetElementBySelector(div, "div:nth-child(4)");
					gameScore = GetElementBySelector(div, "div.sco > a.scorelink");
					if (gameScore == null)
					{
						gameScore = GetElementBySelector(div, "div.sco");
					}
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

		private static IWebElement GetElementBySelector(IWebElement webElement, string selector)
		{
				try
				{
					return webElement.FindElement(By.CssSelector(selector));
				}
				catch(Exception exc)
				{
					Console.WriteLine("Selection Error: " + exc.Message);
					return null;
				}
		}

		private static IWebElement SelectElementFromSetOfSelectors(IWebElement webElement, SelectorDescriptor[] selectors)
		{
				IWebElement selectedWebElement = null; 
				var selectedMode = selectors
								.FirstOrDefault(model => 
								{
									selectedWebElement = GetElementBySelector(webElement, model.Selector);

									if (selectedWebElement != null && 
										!String.IsNullOrWhiteSpace(selectedWebElement.Text))
									{
										return true;
									}

									return false; 
								});
				return selectedWebElement;
		}
    }
}
