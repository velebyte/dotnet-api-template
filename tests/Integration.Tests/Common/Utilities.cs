using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Integration.Tests.Common;

public class Utilities
{
    public static void InitializeDbForTests(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();

        var userEmail = "string@mail.com";
        var user = new ApplicationUser("String", "Stringovich", userEmail)
        {
            Id = Guid.NewGuid(),
            UserName = userEmail,
            NormalizedEmail = userEmail.ToUpperInvariant()
        };

        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Password123!");

        db.Users.Add(user);
        db.SaveChanges();
    }
}
