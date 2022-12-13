using Application.Features.Flowers.CreateFlower;
using Application.Features.Flowers.GetFlower;
using Application.Features.Flowers.GetFlowers;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Authorize]
public class FlowersController : ApiControllerBase
{
    private readonly ISender _mediator;
    public FlowersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(CreateFlowerCommand command) =>
        Created(nameof(Get), await _mediator.Send(command));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid id) =>
        Ok(await _mediator.Send(new GetFlowerQuery(id)));

    [HttpGet]
    public async Task<ActionResult> Get() =>
        Ok(await _mediator.Send(new GetFlowersQuery()));
}