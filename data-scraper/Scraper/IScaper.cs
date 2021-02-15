using OpenQA.Selenium;
using DataScraper.Model;

namespace DataScraper.Scraper
{
	public interface IScraper
	{
		public int Order { get; }
		
		public void ScrapeData(IWebElement webElement, GameData gameData);	
	}	
}
