namespace StendenCafe.Models
{
    public class CafeUser
    {
        public string? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string? Location { get; set; }
        public DateTime Date { get; set; }
    }
}
