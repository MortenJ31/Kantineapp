using MongoDB.Bson.Serialization.Attributes; 
public class LoginRequest
{
    [BsonElement("Email")]
    public string Email { get; set; } 

    [BsonElement("Password")]
    public string Password { get; set; } 
}