using Microsoft.AspNetCore.Identity;

namespace PediatriNobetYonetimSistemi.Data
{
    public static class DatabaseInitializer
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);  // Hata burada çözülmüş olacak
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                await userManager.CreateAsync(adminUser, "AdminPassword123!");  // Güçlü bir şifre belirleyin
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
