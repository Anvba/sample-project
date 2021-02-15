using System;
using OpenQA.Selenium;
using DataScraper.Model;
using DataScraper.Extension;
using DataScraper.Scraper;
using DataScraper.Logging;

namespace DataScraper.WebResourceConsumer
{
	public class WebResource : IWebResource, IDisposable
	{	
		private IOnDataItem _onDataItem;

		private IWebDriver _webDriver;

		private IScraper _pageScraper;

		private ScraperDataModel _scraperDataModel;

		private ILogger _logger;

		public WebResource(IOnDataItem onDataItem, ILogger logger)	
		{
			_onDataItem = onDataItem;
			_logger = logger;
		}
		
		public void Initialize(ScraperDataModel scraperDataModel)
		{
            _logger.LogInfo("Starting test console app");
			
			_scraperDataModel = scraperDataModel;
			_webDriver = scraperDataModel.Url.GetChromeDriver();
					
			_logger.LogInfo("Page titile " + _webDriver.Title);

			_pageScraper = new PageScraper(new IScraper[]
							{
								new HeaderDataScraper(scraperDataModel.GameConatryAndDate, _logger),
								new GameRowScraper(scraperDataModel.GameData, _logger)
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
