using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Core.Models
{
    public class Bruger
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public string Navn { get; set; } = "";
        public string Email { get; set; } = "";
        public string Adgangskode { get; set; } = "";
        [BsonRepresentation(BsonType.String)]
        public Rolle Rolle { get; set; } 
        public List<string> MineKompetencer { get; set; }
        public string Notifikationsmetode { get; set; }
    }
    public enum Rolle
    {
        Administrator,
        Kantineleder,
        Medarbejder
    }

}