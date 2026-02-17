using Exam.App.Domain;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Infrastructure.Database;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var Vet1 = new ApplicationUser
        {
            UserName = "john",
            Email = "john.doe@example.com",
            Name = "John",
            Surname = "Doe",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(Vet1.UserName) == null)
        {
            await userManager.CreateAsync(Vet1, "John123!");
            await userManager.AddToRoleAsync(Vet1, "Veterinarian");
        }

        var Assistant2 = new ApplicationUser
        {
            UserName = "jane",
            Email = "jane.doe@example.com",
            Name = "Jane",
            Surname = "Doe",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(Assistant2.UserName) == null)
        {
            await userManager.CreateAsync(Assistant2, "Jane123!");
            await userManager.AddToRoleAsync(Assistant2, "Assistant");
        }

        var Owner3 = new ApplicationUser
        {
            UserName = "john2",
            Email = "john.doe@example.com",
            Name = "John",
            Surname = "Doe",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(Owner3.UserName) == null)
        {
            await userManager.CreateAsync(Owner3, "John123!");
            await userManager.AddToRoleAsync(Owner3, "Owner");
        }
    }
}