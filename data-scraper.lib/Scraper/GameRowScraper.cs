using OpenQA.Selenium;
using DataScraper.Extension;
using DataScraper.Model;
using DataScraper.Logging;

namespace DataScraper.Scraper
{
	public class GameRowScraper : IScraper
	{
		public int Order => 2;

		private const string DataAttribute = "class";

		private ILogger _logger;
		
		public GameDataDescriptor _gameDataDescriptor;

		public GameRowScraper(GameDataDescriptor gameDataDescriptor, ILogger logger)
		{
			_gameDataDescriptor = gameDataDescriptor;	
			_logger = logger;
		}	

		public void ScrapeData(IWebElement webElement, GameData gameData)
		{
			var divClassAttribute = webElement.GetAttribute(DataAttribute);

			if (!divClassAttribute.Contains(_gameDataDescriptor.RowDivClass))
			{	
				return;
			}
				
			gameData.GameTime = webElement.GetElementBySelector(_gameDataDescriptor.GameTime, _logger)?.Text;
			gameData.FirstTeam = webElement.GetElementBySelector(_gameDataDescriptor.FirstTeam, _logger)?.Text;
			gameData.SecondTeam = webElement.GetElementBySelector(_gameDataDescriptor.SeconadTeam, _logger)?.Text;
			gameData.GameScore = webElement.SelectElementFromSetOfSelectors(_gameDataDescriptor.GameScore, _logger)?.Text;
		}
	}
}
