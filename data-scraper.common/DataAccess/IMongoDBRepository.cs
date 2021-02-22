using MongoDB.Driver;

namespace DataScraper.DataAccess
{
	public interface IMongoDBRepository<TModel>
	{
		void CreateDocument(TModel webResourceModel);	

		TModel Get(string id);

		IChangeStreamCursor<ChangeStreamDocument<TModel>> Watch();
	}
}
