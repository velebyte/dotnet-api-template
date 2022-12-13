using Application.Common.Interfaces.Persistance;

namespace Application.Features.Flowers.GetFlower;

public record GetFlowerQuery(Guid Id)
    : IRequest<FlowerResponse>;

public class GetFlowerQueryHandler : IRequestHandler<GetFlowerQuery, FlowerResponse>
{
    private readonly IFlowersRepository _repository;

    public GetFlowerQueryHandler(IFlowersRepository repository)
    {
        _repository = repository;
    }

    public async Task<FlowerResponse> Handle(GetFlowerQuery query, CancellationToken cancellationToken)
    {
        var flower = await _repository.GetFlowerBy(f => f.Id == query.Id, cancellationToken);

        if (flower is null)
            throw new NotFoundException("Flower");

        return flower.Map();
    }
}
