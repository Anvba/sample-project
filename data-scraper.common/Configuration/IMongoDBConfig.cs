using System;

namespace DataScraper.DataAccess
{
	public interface IMongoDBConfig
	{
		string ConnectionString { get; set; }

		string DatabaseName { get; set; }

		string CollectionName { get; set; }
	}
}
