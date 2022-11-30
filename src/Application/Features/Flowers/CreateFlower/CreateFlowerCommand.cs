using Application.Common.Interfaces.Persistance;
using Domain.FlowerAggregate;

namespace Application.Features.Flowers.CreateFlower;

public record CreateFlowerCommand(
    string Name,
    string Type) : IRequest<Guid>;

public class CreateFlowerCommandHandler : IRequestHandler<CreateFlowerCommand, Guid>
{
    private readonly IFlowerRepository _repository;

    public CreateFlowerCommandHandler(
        IFlowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateFlowerCommand request, CancellationToken cancellationToken)
    {
        var flowerExists = await _repository.GetFlowerBy(flower => flower.Name == request.Name, cancellationToken);

        if (flowerExists is not null)
            throw new DuplicateException("Flower");


        Flower entity = Flower.Create(
            request.Name,
            request.Type);

        return await _repository.AddAsync(entity, cancellationToken);
    }
}
