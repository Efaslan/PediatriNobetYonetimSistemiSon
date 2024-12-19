using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly DatabaseContext _context;

        public DepartmanController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Departman (Public erişim veya kullanıcı rolü için)
        [AllowAnonymous] // Herkes görebilir
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departman.ToListAsync());
        }

        // GET: Departman/Details/5 (Public erişim veya kullanıcı rolü için)
        [AllowAnonymous] // Herkes görebilir
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FirstOrDefaultAsync(m => m.DepartmanId == id);
            if (departman == null) return NotFound();

            return View(departman);
        }

        // GET: Departman/Create (Admin rolü gerektirir)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Yeni Departman Ekle";
            return View();
        }

        // POST: Departman/Create (Admin rolü gerektirir)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("DepartmanAdi, BolumTanimi, YatakSayisi")] Departman departman)
        {
            if (ModelState.IsValid)
            {
                _context.Departman.Add(departman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departman);
        }

        // GET: Departman/Edit/5 (Admin rolü gerektirir)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FindAsync(id);
            if (departman == null) return NotFound();

            return View(departman);
        }

        // POST: Departman/Edit/5 (Admin rolü gerektirir)

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var departman = _context.Departman.Find(id);
            if (departman == null) return NotFound();
            return View(departman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Departman departman)
        {
            // id ile departman.DepartmanId'nin eşleşmesini kontrol ediyoruz
            if (id != departman.DepartmanId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Veritabanındaki departman nesnesini buluyoruz
                    var departmanToUpdate = await _context.Departman.FindAsync(id);
                    if (departmanToUpdate == null)
                    {
                        return NotFound();
                    }

                    // Güncellenen departman alanlarını mevcut departmanla eşliyoruz
                    departmanToUpdate.DepartmanAdi = departman.DepartmanAdi;
                    departmanToUpdate.BolumTanimi = departman.BolumTanimi;
                    departmanToUpdate.YatakSayisi = departman.YatakSayisi;

                    // Güncellenmiş departmanı veritabanına kaydediyoruz
                    _context.Departman.Update(departmanToUpdate);
                    await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departman.Any(e => e.DepartmanId == departman.DepartmanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Başarıyla güncellenen departmanı listeye yönlendiriyoruz
            }
            return View(departman); // Eğer model geçerli değilse formu tekrar render et
        }

        // GET: Departman/Delete/5 (Admin rolü gerektirir)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departman = await _context.Departman.FirstOrDefaultAsync(m => m.DepartmanId == id);
            if (departman == null) return NotFound();

            return View(departman);
        }

        // POST: Departman/Delete/5 (Admin rolü gerektirir)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departman = await _context.Departman.FindAsync(id);
            if (departman != null)
            {
                _context.Departman.Remove(departman);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> KullaniciDepartmanlari()
        {
            var departmanlar = await _context.Departman.ToListAsync();
            return View(departmanlar);
        }

    }
}
