using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;


namespace Core.Models
{
    public class Bruger
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        [BsonElement("Navn")]
        public string Navn { get; set; } = "";
        [BsonElement("Email")]
        public string Email { get; set; } = "";
        [BsonElement("Adgangskode")]
        public string Adgangskode { get; set; } = "";
        [BsonElement("Rolle")]
        [BsonRepresentation(BsonType.String)]
        public Rolle Rolle { get; set; }
        [BsonElement("MineKompetencer")]
        public List<string> MineKompetencer { get; set; }
        [BsonElement("Notifikationsmetode")]
        public string Notifikationsmetode { get; set; }
    }
    public enum Rolle
    {
        Administrator,
        Kantineleder,
        [EnumMember(Value = "Medarbejder")]
        Medarbejder
    }

}