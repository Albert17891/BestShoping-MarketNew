using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class CardService : ICardService
{
    private readonly IUnitOfWork _unitOfWork;

    public CardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddCardProductsAsync(UserProductCard userProduct, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = await _unitOfWork.ProductRepository.Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (product.Quantity >= userProduct.Quantity)
        {
            product.Quantity -= userProduct.Quantity;

            await _unitOfWork.CardRepository.AddAsync(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }
        else
        {
            throw new Exception("Product Quantity is not enough");
        }

    }

    public async Task<IList<UserProductCard>> GetCardProductsAsync(string userId, CancellationToken token)
    {     

        return await _unitOfWork.CardRepository.Table.Where(x => x.UserId == userId)
                                                 .ToListAsync(token);
    }

    public async Task UpdateCardProductsDecrementAsync(UserProductCard userProduct, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = await _unitOfWork.ProductRepository.Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (userProduct.Quantity >= 0)
        {
            product.Quantity += 1;

            _unitOfWork.CardRepository.Update(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }     

    }

    public async Task UpdateCardProductsIncrementAsync(UserProductCard userProduct, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = await _unitOfWork.ProductRepository.Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (product.Quantity >0)
        {
            product.Quantity -= 1;

            _unitOfWork.CardRepository.Update(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }
        else
        {
            throw new Exception("Product Quantity is not enough");
        }
    }
}
