using System;
using OpenQA.Selenium;
using DataScraper.Extension;
using DataScraper.Model;
using DataScraper.Logging;

namespace DataScraper.Scraper
{
	public class HeaderDataScraper : IScraper
	{
		public int Order => 1;

		private ILogger _logger;

		private const string DataAttribute = "class";
		
		private GameCountryAndDateDescriptor _gameCountryAndDateDescriptor;

		private string _gameDate = String.Empty; 

		private string _gameLeague = String.Empty;

		private string _gameCountry = String.Empty;

		public HeaderDataScraper(GameCountryAndDateDescriptor gameCountryAndDateDescriptor, ILogger logger)
		{
			_gameCountryAndDateDescriptor = gameCountryAndDateDescriptor;	
			_logger = logger;
		}	

		public void ScrapeData(IWebElement webElement, GameData gameData)
		{
			var divClassAttribute = webElement.GetAttribute(DataAttribute);

			if (!divClassAttribute.Contains(_gameCountryAndDateDescriptor.HeadDivClass))
			{	
				FillInGameData(gameData);	

				return;
			}

			var newGameDate = webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameDateSelector, _logger);
			
			if (newGameDate == null)
			{
				_gameDate = String.Empty;
				_gameLeague = String.Empty;
				_gameCountry = String.Empty;	

				return;	
			}

			_gameDate = newGameDate.Text;

			var newGameLeague= webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameLeagueSelector, _logger);
			
			if (newGameLeague != null)
			{
				_gameLeague = newGameLeague.Text;
			}
		
			var newGameCountry = webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameContrySelector, _logger);

			if (newGameCountry != null)
			{	
				_gameCountry = newGameCountry.Text;
			}
		}

		private void FillInGameData(GameData gameData)
		{
			gameData.GameDate = _gameDate;
			gameData.GameCountry = _gameCountry;
			gameData.GameLeague = _gameLeague;
		}
	}
}
