using System;
using DataScraper.Model;
using DataScraper.WebResourceConsumer;

namespace DataScraper
{
	public class OnDataItemImpl : IOnDataItem
	{
		public void OnDataItem(GameData gameData)
		{
		    Console.WriteLine("Game Country: " + gameData.GameCountry);	
		    Console.WriteLine("Game League: " + gameData.GameLeague);	
		    Console.WriteLine("Game Date: " + gameData.GameDate);	
			Console.WriteLine("Game Time: " + gameData.GameTime);
			Console.WriteLine("Game First Team: " + gameData.FirstTeam);
			Console.WriteLine("Game Second Team: " + gameData.SecondTeam);
			Console.WriteLine("Game Score: "+ gameData.GameScore);
			Console.WriteLine();
		}	
	}

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting test console app");
			
			var scraperDataModel = new ScraperDataModel();
			
			var webResource = new WebResource(new OnDataItemImpl());
			webResource.Initialize(scraperDataModel);
			webResource.CollectData();
        }
	}
}
