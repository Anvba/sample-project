using DataScraper.Model;

namespace DataScraper.DataAccess
{
	public interface IScraperModelDataService<TModel> where TModel: ScraperDataModel
	{
		TModel Find(string id);

		void Create(TModel webResourceModel);
	}
}
