using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.Persistance;

public interface IFlowersRepository
{
    /// <summary>
    /// Adds a flower
    /// </summary>
    /// <param name="flower"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Flower> AddAsync(Flower flower, CancellationToken token);

    /// <summary>
    /// Returns a flower queried by a provided predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Flower?> GetFlowerBy(Expression<Func<Flower, bool>> predicate, CancellationToken token);

    /// <summary>
    /// Returns a flower queried by a provided name (case insensitive)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<Flower?> GetFlowerByName(string name, CancellationToken token);

    /// <summary>
    /// Returns all flowers
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<Flower>> GetFlowers(CancellationToken token);
}
