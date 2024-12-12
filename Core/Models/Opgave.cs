using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace Core.Models
{
    public class Opgave
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        [BsonElement("Beskrivelse")]
        public string Beskrivelse { get; set; } = "";
        [BsonElement("Starttid")]
        public string StartTid { get; set; }
        [BsonElement("Sluttid")]
        public string SlutTid { get; set; }
        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; }
        [BsonElement("EventID")]
        [BsonRepresentation(BsonType.String)]
        public string EventId { get; set; } = "";
        [BsonElement("AnsvarligForOpgave")]
        [BsonRepresentation(BsonType.String)]
        public List<string> AnsvarligForOpgave { get; set; }
        [BsonElement("OpgaveType")]
        [BsonRepresentation(BsonType.String)]
        public OpgaveType OpgaveType { get; set; }
    }
}

public enum Status
{
    [EnumMember(Value = "IkkePåbegyndt")]
    [BsonRepresentation(BsonType.String)]
    IkkePåbegyndt,

    [EnumMember(Value = "Påbegyndt")]
    [BsonRepresentation(BsonType.String)]
    Påbegyndt,

    [EnumMember(Value = "Afsluttet")]
    [BsonRepresentation(BsonType.String)]
    Afsluttet
}

public enum OpgaveType
{
    Madlavning,
    Bordopsætning,
    Madservering,
    Dekoration,
    Oprydning
}