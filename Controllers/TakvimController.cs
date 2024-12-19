using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class TakvimController : Controller
    {
        private readonly DatabaseContext _context;

        public TakvimController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var randevular = await _context.Randevu
                .Select(r => new TakvimEvent
                {
                    Title = $"Randevu: {r.Hoca.Ad + " " + r.Hoca.Soyad} ve {r.Asistan.Ad + " " + r.Asistan.Soyad}",
                    Start = r.Tarih,
                    End = null, // Randevular için bitiş saati yok
                    AllDay = false
                })
                .ToListAsync();

            var nobetler = await _context.Nobet
                .Select(n => new TakvimEvent
                {
                    Title = $"{n.Departman.DepartmanAdi} Nöbeti: {n.Asistan.Ad + " " + n.Asistan.Soyad}",
                    Start = n.BaslamaTarihi + n.BaslamaSaati,
                    End = n.BitisTarihi + n.BitisSaati,
                    AllDay = false
                })
                .ToListAsync();

            var events = randevular.Concat(nobetler); // Artık aynı türde oldukları için Concat çalışır

            return Json(events);
        }
    }
}
