using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
