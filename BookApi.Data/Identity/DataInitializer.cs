using BookApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookApi.Data.Identity;

public class DataInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        
        await SeedRoles(services);
        await SeedUsers(services);
    }

    private static async Task SeedRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            
        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));
    }

    private static async Task SeedUsers(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        
        var admin = new User { Email = "admin@admin.com", UserName = "admin@admin.com" };
        if (await userManager.FindByEmailAsync(admin.Email) == null)
        {
            await userManager.CreateAsync(admin, "admin123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
        
        var user = new User { Email = "user@user.com", UserName = "user@user.com" };
        if (await userManager.FindByEmailAsync(user.Email) == null)
        {
            await userManager.CreateAsync(user, "user123");
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}