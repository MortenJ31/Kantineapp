using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class LoginResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BrugerId { get; set; } = string.Empty;

        [BsonElement("isSuccessful")] public bool IsSuccessful { get; set; }

        [BsonElement("navn")] public string Navn { get; set; } = string.Empty;

        [BsonElement("email")] public string Email { get; set; } = string.Empty;

        [BsonElement("role")] public string Role { get; set; } = string.Empty;

        [BsonElement("message")] public string Message { get; set; } = string.Empty;
    }

}
