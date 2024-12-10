namespace Core.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string Navn { get; set; } = "";
        public string Email { get; set; } = "";
        public string Adgangskode { get; set; } = "";
        public Rolle BrugerRolle { get; set; } 
    }
    public enum Rolle
    {
        Administrator,
        Kantineleder,
        Medarbejder
    }

}