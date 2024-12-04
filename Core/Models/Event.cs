namespace Core.Models
{
    public class Event
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Dato { get; set; }
        public string Sted { get; set; } = "";
        public int DeltagerAntal { get; set; }
    }
}