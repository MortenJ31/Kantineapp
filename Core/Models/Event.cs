using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Dato { get; set; }
        public string Lokation { get; set; } = "";
        public int DeltagerAntal { get; set; }
        public string MadValg { get; set; } = "";
        public string SærligeØnsker { get; set; } = "";
        public string Kunde { get; set; } = "";
        [BsonRepresentation(BsonType.String)]
        public string BrugerID { get; set; }
    }
}