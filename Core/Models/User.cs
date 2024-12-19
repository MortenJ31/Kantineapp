using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Models
{
 
    /// Repræsenterer en bruger i systemet, med grundlæggende oplysninger som navn, email, rolle og færdigheder.
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        // Primær nøgle for brugeren, gemt som en string i databasen.
        public string? Id { get; set; } = "";

        [BsonElement("Name")]
        // Brugerens fulde navn.
        public string Name { get; set; } = "";

        [BsonElement("Email")]
        // Brugerens email, bruges også i login
        public string Email { get; set; } = "";

        [BsonElement("Password")]
        // Brugerens adgangskode, bruges også i login
        public string Password { get; set; } = "";

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonElement("Role")]
        [BsonRepresentation(BsonType.String)]
        // Brugerens rolle i systemet, gemt som string i databasen og serialiseret som string i JSON.
        public Role Role { get; set; }

        [BsonElement("MySkills")]
        // Liste over brugerens færdigheder, fx "kok" eller "opvasker".
        public List<string> MySkills { get; set; } = new List<string>();

        [BsonElement("NotificationMethod")]
        // Brugerens foretrukne metode til notifikationer, fx email eller SMS.
        public string NotificationMethod { get; set; } = string.Empty;
    }
    
    /// Roller, som en bruger kan have i systemet.

    public enum Role
    {
        [EnumMember(Value = "Administrator")]
        Administrator, // Har adgang til eventcrud page + overview page

        [EnumMember(Value = "Kantineleder")]
        Kantineleder, // Har adgang til eventitemcrud page + overview page

        [EnumMember(Value = "Medarbejder")]
        Medarbejder // Har adgang til overview page
    }
}
