using System;

namespace DataScraper.DataAccess
{
	public class MongoDBConfig : IMongoDBConfig
	{
		public string ConnectionString { get; set; }

		public string DatabaseName { get; set; }

		public string CollectionName { get; set; }
	}
}
