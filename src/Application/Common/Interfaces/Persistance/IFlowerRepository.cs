using Domain.FlowerAggregate;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistance;

public interface IFlowerRepository
{
    /// <summary>
    /// Adds a flower
    /// </summary>
    /// <param name="flower"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Guid> AddAsync(Flower flower, CancellationToken token);

    /// <summary>
    /// Returns a flower queried by a provided predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Flower?> GetFlowerBy(Expression<Func<Flower, bool>> predicate, CancellationToken token);
}
