using DataScraper.Model;

namespace DataScraper.WebResourceConsumer
{
	public interface IOnDataItem
	{
		void OnDataItem(GameData gameData);
	}
}
