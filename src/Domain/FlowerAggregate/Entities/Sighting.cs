namespace Domain.FlowerAggregate.Entities;

public sealed class Sighting : Entity<Guid>
{
    private Sighting(
        Guid id,
        string location,
        string description) : base(id)
    {
        Location = location;
        Description = description;
    }

    public string Location { get; private set; }
    public string Description { get; private set; }

    public static Sighting Create(string location, string description)
    {
        return new(Guid.NewGuid(), location, description);
    }
}
