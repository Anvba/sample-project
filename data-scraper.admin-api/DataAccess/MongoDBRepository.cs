using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using DataAccess.AdminAPI.Model;
using DataAccess.AdminAPI.DataAccess;

namespace DataScraper.AdminAPI.DataAccess
{
	public class MongoDBRepository<TModel> : IMongoDBRepository<TModel> where TModel : IDocument
	{
		private IMongoDBConfig _mongoDBconfig;

		private ILogger<MongoDBRepository<TModel>> _logger;

		private IMongoCollection<TModel> _scraperDataModelCollection;

		public MongoDBRepository(ILogger<MongoDBRepository<TModel>> logger, IMongoDBConfig mongoDBconfig)
		{	
			_mongoDBconfig = mongoDBconfig;
			_logger = logger;

			 var client = new MongoClient(_mongoDBconfig.ConnectionString);
			 var database= client.GetDatabase(_mongoDBconfig.DatabaseName);
			 _scraperDataModelCollection = database.GetCollection<TModel>(_mongoDBconfig.CollectionName);
		}

		public void CreateDocument(TModel documentModel)
		{
			_logger.LogInformation(string.Format("Mongo DB config: {0},  {1}", _mongoDBconfig.ConnectionString, _mongoDBconfig.DatabaseName));	
			_scraperDataModelCollection.InsertOne(documentModel);
			_logger.LogInformation(string.Format("Document Id: {0}", documentModel.Id));
		}

		public TModel Get(string id) => _scraperDataModelCollection.Find<TModel>(model => model.Id == id).FirstOrDefault();
	}
}
