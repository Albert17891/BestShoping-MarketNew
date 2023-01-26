using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces;

public interface IProductRepository
{
    IQueryable<Product> Table { get; }

    Task AddProductAsync(Product product);
    void UpdateProduct(Product product);
}
