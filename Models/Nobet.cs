using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Nobet
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [ForeignKey("AsistanId")]
        public int AsistanId { get; set; } // Foreign Key
        public Asistan Asistan { get; set; } // Navigation property
        [ForeignKey("DepartmanId")]
        public int DepartmanId { get; set; } // Foreign Key
        public Departman Departman { get; set; } // Navigation property
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
