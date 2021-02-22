namespace DataScraper.DataAccess
{
	public interface IMongoDBGameRepository<TModel> : IMongoDBRepository<TModel> where TModel : IGameDocument
	{
		TModel Find(string uid);
	}
}
