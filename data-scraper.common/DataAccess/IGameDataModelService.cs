using DataScraper.Model;

namespace DataScraper.DataAccess
{
	public interface IGameDataModelService<TModel> where TModel: GameData
	{
		TModel Find(string id);
	}
}
