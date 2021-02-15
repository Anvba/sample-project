using OpenQA.Selenium;
using DataScraper.Extension;
using DataScraper.Model;

namespace DataScraper.Scraper
{
	public class GameRowScraper : IScraper
	{
		public int Order => 2;

		private const string DataAttribute = "class";
		
		public GameDataDescriptor _gameDataDescriptor;

		public GameRowScraper(GameDataDescriptor gameDataDescriptor)
		{
			_gameDataDescriptor = gameDataDescriptor;	
		}	

		public void ScrapeData(IWebElement webElement, GameData gameData)
		{
			var divClassAttribute = webElement.GetAttribute(DataAttribute);

			if (!divClassAttribute.Contains(_gameDataDescriptor.RowDivClass))
			{	
				return;
			}
				
			gameData.GameTime = webElement.GetElementBySelector(_gameDataDescriptor.GameTime)?.Text;
			gameData.FirstTeam = webElement.GetElementBySelector(_gameDataDescriptor.FirstTeam)?.Text;
			gameData.SecondTeam = webElement.GetElementBySelector(_gameDataDescriptor.SeconadTeam)?.Text;
			gameData.GameScore = webElement.SelectElementFromSetOfSelectors(_gameDataDescriptor.GameScore)?.Text;
		}
	}
}
