using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Application.Features.Flowers.CreateFlower;

public record CreateFlowerCommand(
    string Name,
    string Type) : IRequest<Guid>;

public class CreateFlowerCommandHandler : IRequestHandler<CreateFlowerCommand, Guid>
{
    private readonly IFlowersRepository _repository;

    public CreateFlowerCommandHandler(
        IFlowersRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateFlowerCommand request, CancellationToken cancellationToken)
    {
        var flowerExists = await _repository.GetFlowerByName(request.Name, cancellationToken);

        if (flowerExists is not null)
            throw new DuplicateException("Flower");


        Flower entity = Flower.Create(
            request.Name,
            request.Type);

        var flower = await _repository.AddAsync(entity, cancellationToken);
        return flower.Id;
    }
}
