using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
    public class Opgave
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        public string Beskrivelse { get; set; } = "";
        public string StartTid { get; set; }
        public string SlutTid { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; } 
        [BsonRepresentation(BsonType.String)]
        public string EventId { get; set; } = "";
        [BsonRepresentation(BsonType.String)]
        public string AnsvarligForOpgave { get; set; }
        [BsonRepresentation(BsonType.String)]
        public OpgaveType OpgaveType { get; set; }
    }
}

public enum Status
{
    IkkePåbegyndt,
    Igang, 
    Færdig
}

public enum OpgaveType
{
    Madlavning,
    Bordopsætning,
    Madservering,
    Dekoration,
    Oprydning
}