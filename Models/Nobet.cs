using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Nobet
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey("AsistanId")]
        public int AsistanId { get; set; } 
        public Asistan Asistan { get; set; } 
        [ForeignKey("DepartmanId")]
        public int DepartmanId { get; set; } 
        public Departman Departman { get; set; } 
        [Required]
        public TimeSpan BaslamaSaati { get; set; } 
        [Required]
        public TimeSpan BitisSaati { get; set; } 
        [Required]
        public DateTime BaslamaTarihi { get; set; } 
        [Required]
        public DateTime BitisTarihi { get; set; } 
    }
}
