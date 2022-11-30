namespace Domain.Common;

public abstract class Entity<T>
{
    public T Id { get; set; }

    protected Entity(T id)
    {
        Id = id;
    }
}
