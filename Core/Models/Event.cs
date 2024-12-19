using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name{ get; set; } = "";
        [BsonElement("Date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; } = "";
        [BsonElement("Participants")]
        public int Participants { get; set; }
        [BsonElement("FoodChoice")]
        public string FoodChoice { get; set; } = "";
        [BsonElement("SpecialRequests")]
        public string SpecialRequests { get; set; } = "";
        [BsonElement("Customer")]
        public string Customer { get; set; } = "";
        [BsonElement("BrugerID")]
        [BsonRepresentation(BsonType.String)]
        public string? UserID { get; set; }
    }
}