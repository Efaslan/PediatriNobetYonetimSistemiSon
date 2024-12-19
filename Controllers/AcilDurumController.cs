using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Helper;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class AcilDurumController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMailHelper _mailHelper;

        public AcilDurumController(DatabaseContext context, IMailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }

        public IActionResult Acil()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Yayinla(string durum, string mesaj)
        {
            if (string.IsNullOrEmpty(mesaj) || string.IsNullOrEmpty(durum))
            {
                ViewBag.Mesaj = "Durum ve Mesaj boş olamaz!";
                return View("Acil");
            }

            var acilDurum = new AcilDurum
            {
                Durum = durum,
                Aciklama = mesaj
            };

            _context.AcilDurum.Add(acilDurum);
            _context.SaveChanges();

            var emailList = _context.Hoca
                .Select(h => h.Mail)
                .Concat(_context.Asistan.Select(a => a.Mail))
                .Where(email => !string.IsNullOrEmpty(email))
                .ToList();

            foreach (var email in emailList)
            {
                _mailHelper.Gonder(email, $"Acil Durum: {durum}", mesaj);
            }

            ViewBag.Mesaj = "Acil durum başarıyla yayınlandı, mail gönderildi ve kaydedildi.";
            return View("Acil");
        }

        public IActionResult Index()
        {
            var acilDurumlar = _context.AcilDurum.ToList();
            return View(acilDurumlar);
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var acilDurum = _context.AcilDurum.Find(id);
            if (acilDurum != null)
            {
                _context.AcilDurum.Remove(acilDurum);
                _context.SaveChanges();
                TempData["Mesaj"] = "Acil durum başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
