using MarketPlace.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infastructure.Data;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> Table
    { get { return _dbSet; } }

    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _context.Remove(entity);
    }

    public void Update(T entity)
    {
        //_context.Attach<T>(entity).State = EntityState.Modified;
        _context.Update(entity);
    }
}
