using System.Collections.Generic;
using OpenQA.Selenium;
using DataScraper.Model;

namespace DataScraper.Scraper
{
	public class PageScraper : IScraper
	{
		public int Order => 0;
		
		private IEnumerable<IScraper> _scrapers;
		
		public PageScraper(IEnumerable<IScraper> scrapers)
		{
			_scrapers = scrapers;	
		}		

		public void ScrapeData(IWebElement webElement, GameData gameData)
		{
			foreach(var scraper in _scrapers)	
			{
				scraper.ScrapeData(webElement, gameData);
			}
		}
	}
}
