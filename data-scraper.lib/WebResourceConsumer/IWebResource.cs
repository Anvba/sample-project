using System;
using System.Threading.Tasks;
using DataScraper.Model;

namespace DataScraper.WebResourceConsumer
{
	public interface IWebResource
	{	
		Task CollectData();

		void Initialize(ScraperDataModel scraperDataModel, string remoteDriverDomainName, bool driverType);
	}
}
