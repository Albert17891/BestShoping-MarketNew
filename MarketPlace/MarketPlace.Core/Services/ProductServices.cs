using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;
public class ProductServices : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<int> AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _unitOfWork.Repository<Product>().AddAsync(product);

        await _unitOfWork.SaveChangeAsync();

        return product.Id;
    }

    public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _unitOfWork.Repository<Product>().Remove(product);

        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<IList<Product>> GetMyProductsAsync(string userId, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Table.Where(x => x.OwnerUserId == userId)
                                                               .ToListAsync(cancellationToken);

        return products;                                                          
    }

    public async Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var products = await _unitOfWork.Repository<Product>().Table.ToListAsync();

        return products;
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _unitOfWork.Repository<Product>().Update(product);

        await _unitOfWork.SaveChangeAsync();
    }
}
