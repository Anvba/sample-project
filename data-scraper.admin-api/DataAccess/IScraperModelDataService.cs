using DataAccess.AdminAPI.Model;
using DataScraper.Model;

namespace DataScraper.AdminAPI.DataAccess
{
	public interface IScraperModelDataService<TModel> where TModel: ScraperDataModel
	{
		public TModel Find(string id);

		public void Create(TModel webResourceModel);
	}
}
