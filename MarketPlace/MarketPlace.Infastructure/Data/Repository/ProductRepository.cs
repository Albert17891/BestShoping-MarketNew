using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;

namespace MarketPlace.Infastructure.Data.Repository;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }   
}