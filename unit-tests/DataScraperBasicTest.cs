using System;
using Xunit; 
using DataScraper.Model; 
using DataScraper.WebResourceConsumer; 
using DataScraper.Logging;

namespace DataScraper.UnitTests 
{
    public class Test_PageScraper
    {
		private static GameData NotEmptyGameData = null;
		
		public class OnDataItemImpl : IOnDataItem
		{
			public void OnDataItem(GameData gameData)
			{
				if (Test_PageScraper.NotEmptyGameData != null)
						return;

				if (!String.IsNullOrWhiteSpace(gameData.GameCountry) &&
					!String.IsNullOrWhiteSpace(gameData.GameLeague) &&	
					!String.IsNullOrWhiteSpace(gameData.GameDate) &&
					!String.IsNullOrWhiteSpace(gameData.GameTime) &&
					!String.IsNullOrWhiteSpace(gameData.FirstTeam) &&
					!String.IsNullOrWhiteSpace(gameData.SecondTeam) &&
					!String.IsNullOrWhiteSpace(gameData.GameScore))
				{
					NotEmptyGameData = new GameData
					{
						GameCountry = gameData.GameCountry,
				 		GameLeague = gameData.GameLeague,
						GameDate = gameData.GameDate,
						GameTime = gameData.GameTime,
						FirstTeam = gameData.FirstTeam,
						SecondTeam = gameData.SecondTeam,
						GameScore = gameData.GameScore
					};
				}	
			} 
		}

		[Fact]
		public void Test_AtLeastOne_GameScraped() 
		{
		    var scraperDataModel = new ScraperDataModel(); 
			var logger = new Logger();
			var webResource = new WebResource(new OnDataItemImpl(), logger);

			webResource.Initialize(scraperDataModel);
			webResource.CollectData();

			Assert.NotNull(Test_PageScraper.NotEmptyGameData);
			Assert.False(Test_PageScraper.NotEmptyGameData.GameCountry.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.FirstTeam.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.GameDate.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.GameLeague.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.GameScore.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.GameTime.Equals(String.Empty));
			Assert.False(Test_PageScraper.NotEmptyGameData.SecondTeam.Equals(String.Empty));

		    logger.LogInfo("Game Country: " + Test_PageScraper.NotEmptyGameData.GameCountry);	
		    logger.LogInfo("Game League: " + Test_PageScraper.NotEmptyGameData.GameLeague);	
		    logger.LogInfo("Game Date: " + Test_PageScraper.NotEmptyGameData.GameDate);	
			logger.LogInfo("Game Time: " + Test_PageScraper.NotEmptyGameData.GameTime);
			logger.LogInfo("Game First Team: " + Test_PageScraper.NotEmptyGameData.FirstTeam);
			logger.LogInfo("Game Second Team: " + Test_PageScraper.NotEmptyGameData.SecondTeam);
			logger.LogInfo("Game Score: "+ Test_PageScraper.NotEmptyGameData.GameScore);
		}	
    }
}
