using Application.Features.Authentication;

namespace Application.Common.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Register(string firstName, string lastName, string email, string password);
    Task<AuthenticationResponse> Login(string email, string password);
}
