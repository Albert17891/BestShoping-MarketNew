using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Repository;

public interface IProductRepository
{
    IQueryable<Product> Table { get; }

    Task AddAsync(Product product);
    void Update(Product product);
}
