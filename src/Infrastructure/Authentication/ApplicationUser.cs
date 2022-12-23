using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public ApplicationUser(
        string firstName, 
        string lastName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
