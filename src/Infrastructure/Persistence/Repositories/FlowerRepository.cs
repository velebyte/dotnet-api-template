using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.FlowerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class FlowerRepository : IFlowerRepository
{
    private readonly ApplicationDbContext _context;

    public FlowerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Flower flower, CancellationToken token)
    {
        var flowerEnty = await _context.Flowers.AddAsync(flower, token);
        var numberOfEntries = await _context.SaveChangesAsync(token);

        if (numberOfEntries < 1)
            throw new DatabaseException();

        return flowerEnty.Entity.Id;
    }

    public async Task<Flower?> GetFlowerBy(Expression<Func<Flower, bool>> predicate, CancellationToken token)
    {
        return await _context.Flowers.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken: token);
    }
}
