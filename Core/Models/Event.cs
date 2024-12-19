using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Models
{
 
    /// Repræsenterer et event med detaljer som navn, dato, lokation og tilhørende information.
    
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        // Eventets unikke ID, gemmes som en string i databasen.
        public string? Id { get; set; }

        [BsonElement("Name")]
        // Navnet på eventet, fx "Juleafslutning" eller "Fredagsbar".
        public string Name { get; set; } = "";

        [BsonElement("Date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        // Dato for eventet, gemt som UTC for konsistens i databasen.
        public DateTime Date { get; set; }

        [BsonElement("Location")]
        // Lokationen for eventet, fx "København" eller "Aarhus".
        public string Location { get; set; } = "";

        [BsonElement("Participants")]
        // Antallet af deltagere i eventet.
        public int Participants { get; set; }

        [BsonElement("FoodChoice")]
        // Madvalg til eventet, fx "Buffet" eller "3-retters menu".
        public string FoodChoice { get; set; } = "";

        [BsonElement("SpecialRequests")]
        // Eventuelle særlige ønsker, fx Glutenfri
        public string SpecialRequests { get; set; } = "";

        [BsonElement("Customer")]
        // Kunden, der har bestilt eventet, fx "Firma X" eller "Lektiecafeen".
        public string Customer { get; set; } = "";

        [BsonElement("UserID")]
        [BsonRepresentation(BsonType.String)]
        // ID for den bruger, der har oprettet eventet (admin)
        public string? UserID { get; set; }
    }
}