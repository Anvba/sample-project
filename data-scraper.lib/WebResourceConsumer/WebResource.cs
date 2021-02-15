using System;
using OpenQA.Selenium;
using DataScraper.Model;
using DataScraper.Extension;
using DataScraper.Scraper;

namespace DataScraper.WebResourceConsumer
{
	public class WebResource : IWebResource, IDisposable
	{	
		private IOnDataItem _onDataItem;

		private IWebDriver _webDriver;

		private IScraper _pageScraper;

		private ScraperDataModel _scraperDataModel;

		public WebResource(IOnDataItem onDataItem)	
		{
			_onDataItem = onDataItem;
		}
		
		public void Initialize(ScraperDataModel scraperDataModel)
		{
            Console.WriteLine("Starting test console app");
			
			_scraperDataModel = scraperDataModel;
			_webDriver = scraperDataModel.Url.GetChromeDriver();
					
			Console.WriteLine("Page titile " + _webDriver.Title);

			_pageScraper = new PageScraper(new IScraper[]
							{
								new HeaderDataScraper(scraperDataModel.GameConatryAndDate),
								new GameRowScraper(scraperDataModel.GameData)
							});
			
		}

		public void CollectData()
		{
			if (_webDriver == null || _pageScraper == null)
				throw new Exception("Instance was not initialized");

			var gameData = new GameData();
			var contentDivs = _webDriver.FindElements(By.CssSelector(_scraperDataModel.ContentSelector));

			foreach(var div in contentDivs)
			{
				_pageScraper.ScrapeData(div, gameData);	
				_onDataItem.OnDataItem(gameData);
			}
		}
		
		public void Dispose()
		{
			if (_webDriver == null)
				return;

			_webDriver.Quit();
			_webDriver = null;
		}
	}		
}
