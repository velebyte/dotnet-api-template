using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Application.Features.Flowers.GetFlowers;

public record GetFlowersQuery()
    : IRequest<List<FlowerResponse>>;

public class GetFlowerQueryHandler : IRequestHandler<GetFlowersQuery, List<FlowerResponse>>
{
    private readonly IFlowersRepository _repository;

    public GetFlowerQueryHandler(IFlowersRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FlowerResponse>> Handle(GetFlowersQuery query, CancellationToken cancellationToken)
    {
        var flowers = await _repository.GetFlowers(cancellationToken);

        return flowers.ConvertAll(new Converter<Flower, FlowerResponse>(x => x.Map()));
    }
}
