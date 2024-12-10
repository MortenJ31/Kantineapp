namespace Core.Models
{
    public class Events
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime Dato { get; set; }
        public string Lokation { get; set; } = "";
        public int DeltagerAntal { get; set; }
        public string MadValg { get; set; } = "";
        public string SærligeØnsker { get; set; } = "";
        public string Kunde { get; set; } = "";
        public List<Opgave> Opgaver { get; set; } = new(); 
    }
}