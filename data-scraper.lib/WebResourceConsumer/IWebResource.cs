using System;
using DataScraper.Model;

namespace DataScraper.WebResourceConsumer
{
	public interface IWebResource
	{	
		void CollectData();

		void Initialize(ScraperDataModel scraperDataModel, string remoteDriverDomainName, bool driverType);
	}
}
