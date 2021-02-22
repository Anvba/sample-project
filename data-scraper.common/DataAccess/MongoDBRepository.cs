using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DataScraper.DataAccess
{
	public class MongoDBRepository<TModel> : IMongoDBRepository<TModel> where TModel : IDocument
	{
		private IMongoDBConfig _mongoDBconfig;

		private ILogger<MongoDBRepository<TModel>> _logger;

		private IMongoCollection<TModel> _mongoCollection;

		public MongoDBRepository(ILogger<MongoDBRepository<TModel>> logger, IMongoDBConfig mongoDBconfig)
		{	
			_mongoDBconfig = mongoDBconfig;
			_logger = logger;

			 var client = new MongoClient(_mongoDBconfig.ConnectionString);
			 var database= client.GetDatabase(_mongoDBconfig.DatabaseName);
			 _mongoCollection = database.GetCollection<TModel>(_mongoDBconfig.CollectionName);
		}

		public void CreateDocument(TModel documentModel)
		{
			_logger.LogInformation(string.Format("Mongo DB config: {0},  {1}", _mongoDBconfig.ConnectionString, _mongoDBconfig.DatabaseName));	
			_mongoCollection.InsertOne(documentModel);
			_logger.LogInformation(string.Format("Document Id: {0}", documentModel.Id));
		}

		public TModel Get(string id) => _mongoCollection.Find<TModel>(model => model.Id == id).FirstOrDefault();

		public IChangeStreamCursor<ChangeStreamDocument<TModel>> Watch()
		{
			var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
			var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<TModel>>().Match("{ operationType: { $in: [ 'insert', 'delete' ] } }");
			return _mongoCollection.Watch<ChangeStreamDocument<TModel>>(pipeline, options);
		}
	}
}
