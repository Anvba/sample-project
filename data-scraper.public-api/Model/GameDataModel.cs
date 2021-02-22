using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataScraper.Model;
using DataScraper.DataAccess;


namespace DataScraper.PublicApi.Model
{
	public class GameDataModel : GameData, IDocument
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
	}
}
