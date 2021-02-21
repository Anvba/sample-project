using System;

namespace DataScraper.AdminAPI.DataAccess
{
	public interface IMongoDBConfig
	{
		string ConnectionString { get; set; }

		string DatabaseName { get; set; }

		string CollectionName { get; set; }
	}
}
