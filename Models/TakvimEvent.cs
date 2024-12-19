namespace PediatriNobetYonetimSistemi.Models
{
    public class TakvimEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; } // End optional
        public bool AllDay { get; set; }
    }
}
