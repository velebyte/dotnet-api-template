using Application.Features.Authentication;
using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using Integration.Tests.Common;
using System.Net;
using System.Net.Http.Json;

namespace Integration.Tests;

public class AuthEndpointsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _webAppFactory;
    private readonly HttpClient _httpClient;

    public AuthEndpointsIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _webAppFactory = factory;
        _httpClient = _webAppFactory.CreateClient();
    }

    [Fact]
    public async Task RegisterEndpoint_ValidData_ReturnsCreated()
    {
        var registerUserCommand = new RegisterUserCommand("Int", "Intovich", "int@mail.com", "Password321!");

        // ACT
        var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerUserCommand);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        Assert.NotNull(authenticationResponse);
    }

    [Fact]
    public async Task LoginEndpoint_ValidCredentials_ReturnsOk()
    {
        var loginUserCommand = new LoginUserCommand("string@mail.com", "Password123!");
         
        // ACT
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginUserCommand);

        Assert.NotNull(response);
        var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(authenticationResponse);
    }
}