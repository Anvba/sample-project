using DataScraper.Model;

namespace DataScraper.DataAccess
{
	public class ScraperModelDataService<TModel> : IScraperModelDataService<TModel> where TModel: ScraperDataModel
	{
		private IMongoDBRepository<TModel> _mongoDBRepository;

		public ScraperModelDataService(IMongoDBRepository<TModel> mongoDBRepository)
		{	
			_mongoDBRepository = mongoDBRepository;	
		}
		
		public TModel Find(string id) => _mongoDBRepository.Get(id);
		
		public void Create(TModel webResourceModel) => _mongoDBRepository.CreateDocument(webResourceModel);
	}
}
