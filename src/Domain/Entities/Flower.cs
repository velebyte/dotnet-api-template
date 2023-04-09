namespace Domain.Entities;

public sealed class Flower : Entity<Guid>
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public List<Sighting> Sightings { get; set; }

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
