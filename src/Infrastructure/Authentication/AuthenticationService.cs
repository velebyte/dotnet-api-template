using Application.Common.Exceptions;
using Application.Common.Interfaces.Authentication;
using Application.Features.Authentication;
using Infrastructure.Authentication.JwtGenerator;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<ApplicationUser> _userManager; 

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<ApplicationUser> userManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
    }

    public async Task<AuthenticationResponse> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new InvalidCredentialsException();

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordValid)
            throw new InvalidCredentialsException();

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new AuthenticationResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            email,
            token);
    }

    public async Task<AuthenticationResponse> Register(string firstName, string lastName, string email, string password)
    {
        // Check if user with the same email exists
        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
            throw new DuplicateException("User with that email");

        user = new ApplicationUser(
            firstName,
            lastName,
            email)
        {
            //Set email as default username
            UserName = email
        };

        var identityResult = await _userManager.CreateAsync(user, password);

        if(!identityResult.Succeeded)
            throw new UserCreationFailureException();

        var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);

        return new AuthenticationResponse(
            user.Id,
            firstName,
            lastName,
            email,
            token);
    }
}
