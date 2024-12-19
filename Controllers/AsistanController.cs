using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class AsistanController : Controller
    {
        private readonly DatabaseContext _context;

        public AsistanController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Hoca
        [AllowAnonymous] // Hoca listesini herkes görebilir
        public async Task<IActionResult> Index()
        {
            var asistanlar = await _context.Asistan.Include(h => h.Departman).ToListAsync();
            return View(asistanlar);
        }

        // GET: Hoca/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Departman listesi ViewData'ya ekleniyor
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi");
            return View();
        }

        // POST: Hoca/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Ad, Soyad, Mail, DepartmanId")] Asistan asistan)
        {

            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", asistan.DepartmanId);

            // Veritabanına kaydet
            _context.Add(asistan);
            await _context.SaveChangesAsync();

            // Başarılı işlemden sonra Index sayfasına yönlendir
            return RedirectToAction(nameof(Index));
        }


        // GET: Hoca/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var asistan = await _context.Asistan.FindAsync(id);
            if (asistan == null) return NotFound();

            // Departman listesi ViewData'ya ekleniyor
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", asistan.DepartmanId);
            return View(asistan);
        }

        // POST: Hoca/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Ad, Soyad, Mail, DepartmanId")] Asistan asistan)
        {
            // Eğer id eşleşmiyorsa, NotFound döndür
            if (id != asistan.Id) return NotFound();

            // Model doğrulama hatalarını kontrol et

            // Veritabanındaki veriyi güncelle
            _context.Update(asistan);
            await _context.SaveChangesAsync();

            // Başarılı işlemden sonra Index sayfasına yönlendir
            return RedirectToAction(nameof(Index));

        }

        // GET: Hoca/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var asistan = await _context.Asistan.Include(h => h.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asistan == null) return NotFound();

            return View(asistan);
        }

        // POST: Hoca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistan = await _context.Asistan.FindAsync(id);
            if (asistan != null)
            {
                _context.Asistan.Remove(asistan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous] // Bu sayfa herkes tarafından erişilebilir
        public async Task<IActionResult> KullaniciAsistanlari()
        {
            var asistanlar = await _context.Asistan
                                           .Include(a => a.Departman)
                                           .ToListAsync();

            return View(asistanlar);
        }
    }
}
