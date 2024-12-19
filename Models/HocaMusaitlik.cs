using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PediatriNobetYonetimSistemi.Models
{
    public class HocaMusaitlik
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [ForeignKey("HocaId")]
        public int HocaId { get; set; } // Foreign Key
        public Hoca Hoca { get; set; } // Navigation Property
        public DateTime Tarih { get; set; }
        public TimeSpan BaslamaSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
    }
}
