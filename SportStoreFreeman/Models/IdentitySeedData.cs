using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;

namespace SportStoreFreeman.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Drevont";
        private const string adminPassword = "Bray3636@";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ApiIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<ApiIdentityDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "Bray3636@example.com";
                user.PhoneNumber = "89265687845";
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
