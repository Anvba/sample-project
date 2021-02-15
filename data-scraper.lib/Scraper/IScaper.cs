using OpenQA.Selenium;
using DataScraper.Model;

namespace DataScraper.Scraper
{
	public interface IScraper
	{
		int Order { get; }
		
		void ScrapeData(IWebElement webElement, GameData gameData);	
	}	
}
