using DataAccess.AdminAPI.Model;

namespace DataScraper.AdminAPI.DataAccess
{
	public interface IMongoDBRepository<TModel>
	{
		void CreateDocument(TModel webResourceModel);	

		TModel Get(string id);
	}
}
