namespace Domain.Entities;

public sealed class Sighting : Entity<Guid>
{
    public string Location { get; private set; }
    public string Description { get; private set; }
    public Guid FlowerId { get; private set; }
    public Flower? Flower { get; private set; }

    private Sighting(
        Guid id,
        Guid flowerId,
        string location,
        string description) : base(id)
    {
        FlowerId = flowerId;
        Location = location;
        Description = description;

    }

    public static Sighting Create(Guid flowerId, string location, string description)
    {
        return new(Guid.NewGuid(), flowerId, location, description);
    }
}
