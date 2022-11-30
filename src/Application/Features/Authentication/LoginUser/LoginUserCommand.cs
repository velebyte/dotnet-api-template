using Application.Common.Interfaces.Authentication;

namespace Application.Features.Authentication.LoginUser;

public record LoginUserCommand(
    string Email, 
    string Password) : IRequest<AuthenticationResponse>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticationResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.Login(request.Email, request.Password);
    }
}
