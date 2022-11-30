using Application.Common.Exceptions;
using Application.Common.Interfaces.Authentication;
using Application.Features.Authentication;
using Application.Features.Authentication.RegisterUser;
using Moq;

namespace Unit.Tests.Application;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    public RegisterUserCommandHandlerTests()
    {
        _authenticationServiceMock = new();
    }

    [Fact]
    public async void Handle_Should_ReturnAuthenticationResponse_When_UserDataValid()
    {
        // Arrange
        RegisterUserCommand registerUserCommand = new(
            "John",
            "Smith",
            "john@mail.com",
            "Password123!");

        _authenticationServiceMock
            .Setup(x => x.Register(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(new AuthenticationResponse(
                "userId",
                registerUserCommand.FirstName,
                registerUserCommand.LastName,
                registerUserCommand.Email,
                "Token"
            ));

        var commandHandler = new RegisterUserCommandHandler(_authenticationServiceMock.Object);

        // Act
        AuthenticationResponse response = await commandHandler.Handle(registerUserCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Token);
        Assert.Equal(registerUserCommand.Email, response.Email);
    }

    [Fact]
    public async Task Handle_Should_ThrowUserCreationFailureException_When_PasswordIsInvalid()
    {
        // Arrange
        RegisterUserCommand registerUserCommand = new(
            "John",
            "Smith",
            "john@mail.com",
            "password");

        _authenticationServiceMock
            .Setup(x => x.Register(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.Is<string>(x => x == registerUserCommand.Password)))
            .Throws<UserCreationFailureException>();

        var commandHandler = new RegisterUserCommandHandler(_authenticationServiceMock.Object);

        // Act
        async Task act() => await commandHandler.Handle(registerUserCommand, CancellationToken.None);

        // Assert
        UserCreationFailureException exception = await Assert.ThrowsAsync<UserCreationFailureException>(act);
        Assert.NotNull(exception);
        Assert.IsType<UserCreationFailureException>(exception);
    }

    [Fact]
    public async Task Handle_Should_ThrowDuplicateException_When_UserEmailIsDuplicate()
    {
        // Arrange
        RegisterUserCommand registerUserCommand = new(
            string.Empty,
            string.Empty,
            "john@mail.com",
            "Password123!");

        _authenticationServiceMock
            .Setup(x => x.Register(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Throws(new DuplicateException("User"));

        var commandHandler = new RegisterUserCommandHandler(_authenticationServiceMock.Object);

        // Act
        async Task act() => await commandHandler.Handle(registerUserCommand, CancellationToken.None);

        // Assert
        DuplicateException exception = await Assert.ThrowsAsync<DuplicateException>(act);
        Assert.NotNull(exception);
        Assert.IsType<DuplicateException>(exception);
    }
}
