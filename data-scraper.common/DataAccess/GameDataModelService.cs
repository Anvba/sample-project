using DataScraper.Model;

namespace DataScraper.DataAccess
{
	public class GameDataModelService<TModel> : IGameDataModelService<TModel> where TModel: GameData
	{
		private IMongoDBRepository<TModel> _mongoDBRepository;

		public GameDataModelService(IMongoDBRepository<TModel> mongoDBRepository)
		{	
			_mongoDBRepository = mongoDBRepository;	
		}
		
		public TModel Find(string id) => _mongoDBRepository.Get(id);
	}
}
