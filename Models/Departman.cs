using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Departman
    {
        [Key]
        public int DepartmanId { get; set; }

        [Required]
        public string DepartmanAdi { get; set; }

        public string BolumTanimi { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Yatak sayısı geçerli bir sayı olmalıdır.")]
        public int YatakSayisi { get; set; }
    }
}
