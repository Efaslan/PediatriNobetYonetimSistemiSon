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

        [AllowAnonymous] 
        public async Task<IActionResult> Index()
        {
            var hocalar = await _context.Hoca.Include(h => h.Departman).ToListAsync();
            return View(hocalar);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Ad, Soyad, Mail, DepartmanId")] Hoca hoca)
        {
           
            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", hoca.DepartmanId);
                
            _context.Add(hoca);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hoca = await _context.Hoca.FindAsync(id);
            if (hoca == null) return NotFound();

            ViewData["DepartmanId"] = new SelectList(_context.Departman, "DepartmanId", "DepartmanAdi", hoca.DepartmanId);
            return View(hoca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Ad, Soyad, Mail, DepartmanId")] Hoca hoca)
        {
            if (id != hoca.Id) return NotFound();

            
                _context.Update(hoca);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hoca = await _context.Hoca.Include(h => h.Departman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoca == null) return NotFound();

            return View(hoca);
        }

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
