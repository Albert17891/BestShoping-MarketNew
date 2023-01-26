using MarketPlace.Core.Interfaces;

namespace MarketPlace.Infastructure.Data;

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

