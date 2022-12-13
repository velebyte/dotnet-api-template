using Application.Common.Exceptions;
using Application.Features.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Authentication.JwtGenerator;
using Microsoft.AspNetCore.Identity;
using Unit.Tests.Helpers;

namespace Unit.Tests.Infrastructure;

public class AuthenticationServiceTests
{
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

    // Arrange
    private const string firstName = "John";
    private const string lastName = "Smith";
    private const string email = "john.smith@mail.com";
    private const string password = "Password123!";

    public AuthenticationServiceTests()
    {
        _jwtTokenGeneratorMock = new();
        _userManagerMock = MockHelper.MockUserManager<ApplicationUser>();
    }

    [Fact]
    public async void Register_Should_ThrowDuplicateException_When_EmailIsNotUnique()
    {
        // Arrange
        _userManagerMock.Setup(
            x => x
            .FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApplicationUser(firstName, lastName, email));

        var authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _userManagerMock.Object);

        // Act
        async Task act() => await authenticationService.Register(firstName, lastName, email, password);

        // Assert
        DuplicateException exception = await Assert.ThrowsAsync<DuplicateException>(act);
        Assert.NotNull(exception);
        Assert.IsType<DuplicateException>(exception);
    }

    [Fact]
    public async void Register_Should_ThrowUserCreationFailureException_When_PasswordIsInvalid()
    {
        // Arrange
        const string invalidPassword = "password";

        _userManagerMock.Setup(
            x => x
            .CreateAsync(It.IsAny<ApplicationUser>(), invalidPassword))
            .ReturnsAsync(IdentityResult.Failed());

        var authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _userManagerMock.Object);

        // Act
        async Task act() => await authenticationService.Register(firstName, lastName, email, invalidPassword);

        // Assert
        UserCreationFailureException exception = await Assert.ThrowsAsync<UserCreationFailureException>(act);
        Assert.NotNull(exception);
        Assert.IsType<UserCreationFailureException>(exception);
    }

    [Fact]
    public async void Register_Should_ReturnAuthenticationResponse_WhenUserDataIsValid()
    {
        // Arrange
        _userManagerMock.Setup(
            x => x
            .CreateAsync(
                It.IsAny<ApplicationUser>(),
                 password))
            .ReturnsAsync(IdentityResult.Success);

        _jwtTokenGeneratorMock.Setup(
            x => x
            .GenerateToken(
                It.IsAny<string>(),
                firstName,
                lastName))
            .Returns("token");

        var authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _userManagerMock.Object);

        // Act
        AuthenticationResponse response = await authenticationService.Register(firstName, lastName, email, password);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Token);
    }
}