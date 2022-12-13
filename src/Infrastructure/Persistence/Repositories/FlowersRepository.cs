using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.FlowerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class FlowersRepository : IFlowersRepository
{
    private readonly ApplicationDbContext _context;

    public FlowersRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Flower> AddAsync(Flower flower, CancellationToken token)
    {
        var flowerEntry = await _context.Flowers.AddAsync(flower, token);
        var numberOfEntries = await _context.SaveChangesAsync(token);

        if (numberOfEntries < 1)
            throw new DatabaseException();

        return flowerEntry.Entity;
    }

    public async Task<Flower?> GetFlowerBy(Expression<Func<Flower, bool>> predicate, CancellationToken token)
    {
        return await _context.Flowers.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken: token);
    }

    public async Task<Flower?> GetFlowerByName(string name, CancellationToken token)
    {
        return await _context.Flowers.AsNoTracking().FirstOrDefaultAsync(flower => EF.Functions.ILike(flower.Name, name), cancellationToken: token);
    }

    public async Task<List<Flower>> GetFlowers(CancellationToken token)
    {
        return await _context.Flowers.AsNoTracking().ToListAsync(token);
    }
}
