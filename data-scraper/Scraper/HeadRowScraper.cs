using System;
using OpenQA.Selenium;
using DataScraper.Extension;
using DataScraper.Model;

namespace DataScraper.Scraper
{
	public class HeaderDataScraper : IScraper
	{
		public int Order => 1;

		private const string DataAttribute = "class";
		
		private GameCountryAndDateDescriptor _gameCountryAndDateDescriptor;

		private string _gameDate = String.Empty; 

		private string _gameLeague = String.Empty;

		private string _gameCountry = String.Empty;

		public HeaderDataScraper(GameCountryAndDateDescriptor gameCountryAndDateDescriptor)
		{
			_gameCountryAndDateDescriptor = gameCountryAndDateDescriptor;	
		}	

		public void ScrapeData(IWebElement webElement, GameData gameData)
		{
			var divClassAttribute = webElement.GetAttribute(DataAttribute);

			if (divClassAttribute.Contains(_gameCountryAndDateDescriptor.HeadDivClass))
			{	
				FillInGameData(gameData);	

				return;
			}

			var newGameDate = webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameDateSelector);
			
			if (newGameDate == null)
			{
				_gameDate = String.Empty;
				_gameLeague = String.Empty;
				_gameCountry = String.Empty;	

				return;	
			}

			_gameDate = newGameDate.Text;

			var newGameLeague= webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameLeagueSelector);
			
			if (newGameLeague != null)
			{
				_gameLeague = newGameLeague.Text;
			}
		
			var newGameCountry = webElement.SelectElementFromSetOfSelectors(_gameCountryAndDateDescriptor.GameContrySelector);

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
