using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataScraper.Model;
using DataScraper.DataAccess;

namespace DataAccess.AdminAPI.Model
{
	public class WebResourceModel : ScraperDataModel, IDocument
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
	}
}
