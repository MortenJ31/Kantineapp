namespace Core.Models
{
    public class Opgave
    {
        public string OpgaveId { get; set; }
        public string EventId { get; set; }
        public string Navn { get; set; }
        public string Status { get; set; }
        public List<string> MedarbejderIds { get; set; } = new();
    }
}