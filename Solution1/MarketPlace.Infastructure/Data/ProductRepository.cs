using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces;

namespace MarketPlace.Infastructure.Data;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddProductAsync(Product product)
    {
        await AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}