namespace Core.Models
{
    public class Opgave
    {
        public string? Id { get; set; }
        public string Beskrivelse { get; set; } = "";
        public string Status { get; set; } = "Ikke Påbegyndt";
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        
        public string EventId { get; set; } = ""; 
        
        public List<string> AssignedEmployeeIds { get; set; } = new(); 
    }
}