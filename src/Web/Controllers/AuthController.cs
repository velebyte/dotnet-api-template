using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;

namespace Web.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly ISender _mediator;
    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<ActionResult> Post(RegisterUserCommand command) =>
        Ok(await _mediator.Send(command));

    [HttpPost("login")]
    public async Task<ActionResult> Get(LoginUserCommand command) =>
        Ok(await _mediator.Send(command));
}