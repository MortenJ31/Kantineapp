using MongoDB.Bson.Serialization.Attributes; 
public class LoginRequest
    {
        [BsonElement("email")]
        public string Email { get; set; } 

        [BsonElement("adgangskode")]
        public string Adgangskode { get; set; } 
    }

