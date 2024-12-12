using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name{ get; set; } = "";
        [BsonElement("Dato")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Dato { get; set; }
        [BsonElement("Lokation")]
        public string Lokation { get; set; } = "";
        [BsonElement("DeltagerAntal")]
        public int DeltagerAntal { get; set; }
        [BsonElement("MadValg")]
        public string MadValg { get; set; } = "";
        [BsonElement("SærligeØnsker")]
        public string SærligeØnsker { get; set; } = "";
        [BsonElement("Kunde")]
        public string Kunde { get; set; } = "";
        [BsonElement("BrugerID")]
        [BsonRepresentation(BsonType.String)]
        public string BrugerID { get; set; }
    }
}