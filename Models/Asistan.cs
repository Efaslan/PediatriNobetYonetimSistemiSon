using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Asistan
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Mail alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Mail { get; set; }

        [ForeignKey("DepartmanId")]
        public int? DepartmanId { get; set; }

        public Departman Departman { get; set; }
    }
}
