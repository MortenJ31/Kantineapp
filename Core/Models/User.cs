using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }  = "";
        [BsonElement("Name")]
        public string Name{ get; set; } = "";
        [BsonElement("Email")]
        public string Email { get; set; } = "";
        [BsonElement("Password")]
        public string Password { get; set; } = "";
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonElement("Role")]
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }
        [BsonElement("MySkills")]
        public List<string> MySkills { get; set; } = new List<string>();
        [BsonElement("NotificationMethod")]
        public string NotificationMethod { get; set; } = string.Empty;
    }
    public enum Role
    {
        [EnumMember(Value = "Administrator")]
        Administrator,

        [EnumMember(Value = "Kantineleder")]
        Kantineleder,

        [EnumMember(Value = "Medarbejder")]
        Medarbejder
    }


}