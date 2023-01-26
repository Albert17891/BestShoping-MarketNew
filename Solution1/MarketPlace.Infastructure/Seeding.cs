using MarketPlace.Core.Entities;
using MarketPlace.Infastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infastructure;

public static class Seeding
{
    public static async Task SeedUserAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await SeedAdminAsync(userManager);
            Migrate(context);
        }
    }

    private static void Migrate(AppDbContext context)
    {
        context.Database.Migrate();
    }
    private static async Task SeedAdminAsync(UserManager<AppUser> userManager)
    {

        var admin = new AppUser()
        {
            FirstName = "Albert",
            LastName = "Gevorkyan",
            UserName = "Admin",
            Email = "abogevorgian@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != admin.Id))
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                await userManager.CreateAsync(admin, "Aa123456!");
            }
        }
    }
}
