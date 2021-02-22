using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DataScraper.DataAccess
{
	public class MongoDBGameRepository<TModel> : MongoDBRepository<TModel>, IMongoDBGameRepository<TModel> where TModel : IGameDocument
	{
		public MongoDBGameRepository(ILogger<MongoDBRepository<TModel>> logger, IMongoDBConfig mongoDBconfig)
				:base(logger, mongoDBconfig)
		{
		}

		public TModel Find(string uid) => _mongoCollection.Find<TModel>(model => model.UID == uid).FirstOrDefault();
	}
}
