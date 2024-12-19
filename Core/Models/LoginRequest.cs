using MongoDB.Bson.Serialization.Attributes;


/// Repræsenterer en login-anmodning med brugerens email og adgangskode.

public class LoginRequest
{
    [BsonElement("Email")]
    // Brugerens email, som bruges til login.
    public string Email { get; set; }

    [BsonElement("Password")]
    // Brugerens adgangskode til login.
    public string Password { get; set; }
}