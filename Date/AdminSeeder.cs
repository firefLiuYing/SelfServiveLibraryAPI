using AutoLibraryAPI.Models;
using AutoLibraryAPI.Util;
using Microsoft.EntityFrameworkCore;

namespace AutoLibraryAPI.Date;

public static class AdminSeeder
{
    public static void SeedAdminUser(DataContext context,IConfiguration configuration)
    {
        if (context.AdminUsers.Any()) return;
        var adminPassword = configuration["AdminPassword"]??"admin123";
        PasswordHasher.CreatePasswordHash(adminPassword, out var passwordHash, out var passwordSalt);
        var adminUser = new AdminUser()
        {
            Id = 1,
            Username = "admin",
            Email = "admin@library.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = Role.SuperAdmin,
            CanManageUsers = true,
            CanManageSystem = true,
            CanManageBooks = true,
            UserType = "AdminUser",
        };
        context.AdminUsers.Add(adminUser);
        context.SaveChanges();
    }
}