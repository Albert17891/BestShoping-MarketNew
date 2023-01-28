using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces;
public interface IProductService
{
    Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<int> AddProductAsync(Product product, CancellationToken cancellationToken);
}
