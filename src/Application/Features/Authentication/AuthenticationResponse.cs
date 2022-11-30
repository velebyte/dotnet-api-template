namespace Application.Features.Authentication;

public record AuthenticationResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
