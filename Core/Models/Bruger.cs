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
        public string? Id { get; set; }  = "";
        [BsonElement("Navn")]
        public string Navn { get; set; } = "";
        [BsonElement("Email")]
        public string Email { get; set; } = "";
        [BsonElement("Adgangskode")]
        public string Adgangskode { get; set; } = "";
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonElement("Rolle")]
        [BsonRepresentation(BsonType.String)]
        public Rolle Rolle { get; set; }
        [BsonElement("MineKompetencer")]
        public List<string> MineKompetencer { get; set; } = new List<string>();
        [BsonElement("Notifikationsmetode")]
        public string Notifikationsmetode { get; set; } = string.Empty;
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