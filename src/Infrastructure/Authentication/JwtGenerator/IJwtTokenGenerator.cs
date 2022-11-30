namespace Infrastructure.Authentication.JwtGenerator;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string firstName, string lastName);
}
