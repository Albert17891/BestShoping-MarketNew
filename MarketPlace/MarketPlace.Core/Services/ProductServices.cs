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

        await _unitOfWork.ProductRepository.AddProductAsync(product);

        await _unitOfWork.SaveChangeAsync();

        return product.Id;
    }

    public async Task<IList<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var products = await _unitOfWork.ProductRepository.Table.ToListAsync();

        return products;
    }
}
