using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Models
{
    /// Repræsenterer en specifik opgave tilknyttet et event, med ansvarlige personer, tidsplan og status.

    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        // Opgavens unikke ID, gemmes som string i databasen.
        public string? Id { get; set; }

        [BsonElement("Description")]
        // Beskrivelse af opgaven, fx "Forbered salat".
        public string Description { get; set; } = ""; 

        [BsonElement("StartTime")]
        // Starttidspunkt for opgaven (fx "14:00").
        public string StartTime { get; set; } = ""; 

        [BsonElement("EndTime")]
        // Sluttidspunkt for opgaven (fx "15:30").
        public string EndTime { get; set; } = ""; 

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]
        // Status for opgaven, fx "IkkePåbegyndt", gemmes som string i databasen.
        public Status Status { get; set; } = Status.IkkePåbegyndt; 

        [BsonElement("EventID")]
        [BsonRepresentation(BsonType.String)]
        // Refererer til det event, som opgaven er en del af.
        public string EventId { get; set; } = ""; 

        [BsonElement("ResponsibleForTask")]
        [BsonRepresentation(BsonType.String)]
        // Liste af bruger-ID'er, som er ansvarlige for opgaven.
        public List<string> ResponsibleForTask { get; set; } = new List<string>(); 

        [BsonElement("TaskType")]
        [BsonRepresentation(BsonType.String)]
        // Opgavetype, fx "Madlavning" eller "Bordopsætning", gemmes som string.
        public TaskType TaskType { get; set; } = TaskType.Madlavning;
    }

    /// <summary>
    /// Mulige statustyper for en opgave.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value = "IkkePåbegyndt")]
        [BsonRepresentation(BsonType.String)]
        IkkePåbegyndt, // Opgaven er endnu ikke startet.

        [EnumMember(Value = "Påbegyndt")]
        [BsonRepresentation(BsonType.String)]
        Påbegyndt, // Opgaven er i gang.

        [EnumMember(Value = "Afsluttet")]
        [BsonRepresentation(BsonType.String)]
        Afsluttet // Opgaven er færdiggjort.
    }


    /// Typer af opgaver, der kan udføres under et event.

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
