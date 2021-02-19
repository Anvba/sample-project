using System.Threading.Tasks;
using DataScraper.Model;

namespace DataScraper.WebResourceConsumer
{
	public interface IOnDataItem
	{
		Task Initialize();

		Task OnDataItem(GameData gameData);

		Task Destroy();
	}
}
