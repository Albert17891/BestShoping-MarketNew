using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Interfaces.Repository;

namespace MarketPlace.Infastructure.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        ProductRepository = new ProductRepository(_context);
    }
    public IProductRepository ProductRepository { get; }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

