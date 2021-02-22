using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataScraper.Model;
using DataScraper.DataAccess;

namespace DataScraper.Processor
{
	public class GameDataModel : GameData, IGameDocument	
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

		public string UID { get; set; }

		public string Version { get; set; }
	}
}
