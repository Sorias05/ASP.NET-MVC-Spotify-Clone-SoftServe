using Microsoft.AspNetCore.Identity;
using WebPractice.Data.Entities;
using System.Data;

namespace WebPractice.Helpers
{
    static class Seeder
    {
        public static async Task SeedRoles(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdmin(this IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string email = "admin@admin.com";
            string password = "Admin_123";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new User();
                user.UserName = email;
                user.Email = email;

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
