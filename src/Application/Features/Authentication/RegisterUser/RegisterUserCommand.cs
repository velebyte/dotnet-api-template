using Application.Common.Interfaces.Authentication;

namespace Application.Features.Authentication.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResponse>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
    }
}