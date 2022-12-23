namespace Infrastructure.Authentication.JwtGenerator;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName);
}
