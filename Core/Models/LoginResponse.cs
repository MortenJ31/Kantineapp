using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{

    /// Repræsenterer responsen på en login-anmodning.
    /// Indeholder information om, hvorvidt login var vellykket, brugerens rolle og en besked.

    public class LoginResponse
    {
        [BsonElement("isSuccessful")]
        // Angiver, om login blev gennemført succesfuldt (true eller false).
        public bool IsSuccessful { get; set; }
        
        [BsonElement("Role")]
        // Brugerens rolle, f.eks. "Administrator", "Kantineleder" eller "Medarbejder". Bruges til brugerrettigheder
        public string Role { get; set; } = string.Empty;

        [BsonElement("Message")]
        // Besked om login-resultatet, f.eks. "Login vellykket" eller "Forkert adgangskode".
        public string Message { get; set; } = string.Empty;
    }
}