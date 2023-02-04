using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Services;
public interface IProductService
{
    Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<int> AddProductAsync(Product product, CancellationToken cancellationToken);
    Task UpdateProductAsync(Product product,CancellationToken cancellationToken);
    Task DeleteProductAsync(Product product,CancellationToken cancellationToken);

    Task<IList<Product>> GetMyProductsAsync(string userId, CancellationToken cancellationToken);
}
