using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PediatriNobetYonetimSistemi.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Giriş başarılıysa yönlendirme
                }

                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
            }

            return View();
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
