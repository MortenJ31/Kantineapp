using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Bruger
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }  = string.Empty;
        
        [BsonElement("Navn")]
        public string Navn { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } 

        [BsonElement("Adgangskode")]
        public string Adgangskode { get; set; } 

        [JsonConverter(typeof(JsonStringEnumConverter))] // JSON as string
        [BsonElement("Rolle")]
        [BsonRepresentation(BsonType.String)] // MongoDB as string
        public Rolle Rolle { get; set; } = Rolle.Medarbejder;

        [BsonElement("MineKompetencer")] 
        public List<string> MineKompetencer { get; set; } = new List<string>();
        
        [BsonElement("Notifikationsmetode")]
        public string Notifikationsmetode { get; set; }
    }

    public enum Rolle
    {
        [EnumMember(Value = "Administrator")]
        Administrator,

        [EnumMember(Value = "Kantineleder")]
        Kantineleder,

        [EnumMember(Value = "Medarbejder")]
        Medarbejder
    }
}
