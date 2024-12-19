using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; } = ""; 

        [BsonElement("StartTime")]
        public string StartTime { get; set; } = ""; 
        [BsonElement("EndTime")]
        public string EndTime { get; set; } = ""; 

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; } = Status.IkkePåbegyndt; 

        [BsonElement("EventID")]
        [BsonRepresentation(BsonType.String)]
        public string EventId { get; set; } = ""; 

        [BsonElement("ResponsibleForTask")]
        [BsonRepresentation(BsonType.String)]
        public List<string> ResponsibleForTask { get; set; } = new List<string>(); 

        [BsonElement("TaskType")]
        [BsonRepresentation(BsonType.String)]
        public TaskType TaskType { get; set; } = TaskType.Madlavning;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskType
    {
        Madlavning,
        Bordopsætning,
        Madservering,
        Dekoration,
        Oprydning
    }
}
