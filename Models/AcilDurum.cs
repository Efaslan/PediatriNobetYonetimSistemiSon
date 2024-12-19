using System.ComponentModel.DataAnnotations;

namespace PediatriNobetYonetimSistemi.Models
{
    public class AcilDurum
    {
        [Key]
        public int Id { get; set; }
        public string Durum { get; set; }
        public string Aciklama { get; set; }
    }
}
