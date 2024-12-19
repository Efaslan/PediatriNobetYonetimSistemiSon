using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PediatriNobetYonetimSistemi.Data;
using PediatriNobetYonetimSistemi.Models;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class HocaController : Controller
    {
        private readonly DatabaseContext _context;

        public HocaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Hoca
        [AllowAnonymous] // Hoca listesini herkes görebilir
        public async Task<IActionResult> Index()
        {
            var hocalar = await _context.Hoca.Include(h => h.Departman).ToListAsync();
            return View(hocalar);
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
        public async Task<IActionResult> Create([Bind("Ad, Soyad, Mail, DepartmanId")] Hoca hoca)
        {
           
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", hoca.DepartmanId);
                
            // Veritabanına kaydet
            _context.Add(hoca);
            await _context.SaveChangesAsync();

            // Başarılı işlemden sonra Index sayfasına yönlendir
            return RedirectToAction(nameof(Index));
        }


        // GET: Hoca/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hoca = await _context.Hoca.FindAsync(id);
            if (hoca == null) return NotFound();

            // Departman listesi ViewData'ya ekleniyor
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", hoca.DepartmanId);
            return View(hoca);
        }

        // POST: Hoca/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Ad, Soyad, Mail, DepartmanId")] Hoca hoca)
        {
            // Eğer id eşleşmiyorsa, NotFound döndür
            if (id != hoca.Id) return NotFound();

            // Model doğrulama hatalarını kontrol et
            
                // Veritabanındaki veriyi güncelle
                _context.Update(hoca);
                await _context.SaveChangesAsync();

                // Başarılı işlemden sonra Index sayfasına yönlendir
                return RedirectToAction(nameof(Index));
        }

        // GET: Hoca/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hoca = await _context.Hoca.Include(h => h.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoca == null) return NotFound();

            return View(hoca);
        }

        // POST: Hoca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoca = await _context.Hoca.FindAsync(id);
            if (hoca != null)
            {
                _context.Hoca.Remove(hoca);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> KullaniciHocalari()
        {
            var hocalar = await _context.Hoca.Include(h => h.Departman).ToListAsync();
            return View(hocalar);
        }
    }
}
