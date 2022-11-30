using Domain.FlowerAggregate.Entities;

namespace Domain.FlowerAggregate;

public sealed class Flower : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Type { get; private set; }

    private List<Sighting> _sightings = new();
    public IReadOnlyList<Sighting> Sightings => _sightings.AsReadOnly();

    private Flower(
        Guid id,
        string name,
        string type) : base(id)
    {
        Name = name;
        Type = type;
    }

    public static Flower Create(string name, string type)
    {
        return new(Guid.NewGuid(), name, type);
    }
}
