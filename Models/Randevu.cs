using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey("AsistanId")]
        public int AsistanId { get; set; }     
        public Asistan Asistan { get; set; } 
        [ForeignKey("HocaId")]
        public int HocaId { get; set; }       
        public Hoca Hoca { get; set; }
        [Required]
        public DateTime Tarih { get; set; } 
        [Required]
        public TimeSpan Saat { get; set; } 
    }
}
