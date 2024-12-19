using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class LoginResponse
    {
        [BsonElement("isSuccessful")] public bool IsSuccessful { get; set; }
        

        [BsonElement("Role")] public string Role { get; set; } = string.Empty;

        [BsonElement("Message")] public string Message { get; set; } = string.Empty;
    }

}