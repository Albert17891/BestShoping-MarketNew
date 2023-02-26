using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
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

        var product = await _unitOfWork.Repository<Product>().Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (product.Quantity >= userProduct.Quantity)
        {
            product.Quantity -= userProduct.Quantity;

            await _unitOfWork.Repository<UserProductCard>().AddAsync(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }
        else
        {
            throw new Exception("Product Quantity is not enough");
        }

    }

    public async Task DeleteCardProductAsync(int id, CancellationToken token)
    {
        var cardProduct = await _unitOfWork.Repository<UserProductCard>().Table.SingleOrDefaultAsync(x => x.Id == id, token);
        var product = await _unitOfWork.Repository<Product>().Table.SingleOrDefaultAsync(x => x.Id == cardProduct.ProductId);

        if (cardProduct is null)
            throw new Exception("Card Product is Not Exist");

        if (product is null)
            throw new Exception("Product is Not Exist");

        product.Quantity += cardProduct.Quantity;

        //CheckVaucer if it exist change IsBlocked to false
        var vaucer= await CheckVaucerAsync(cardProduct.UserId, cardProduct.ProductId);

        if(vaucer is not null)
        {
            vaucer.IsBlocked = false;
        }

        _unitOfWork.Repository<UserProductCard>().Remove(cardProduct);

        await _unitOfWork.SaveChangeAsync();
    }

    private async Task<Vaucer> CheckVaucerAsync(string userId, int productId)
    {
        var vaucer = await _unitOfWork.Repository<Vaucer>().Table.Where(x => x.ExpireTime > DateTime.Now && x.IsBlocked == true)
                                                                 .Where(x => x.UserId == userId && x.ProductId == productId)
                                                                 .SingleOrDefaultAsync();

        return vaucer;
    }

    public async Task<IList<UserProductCard>> GetCardProductsAsync(string userId, CancellationToken token)
    {

        return await _unitOfWork.Repository<UserProductCard>().Table.Where(x => x.UserId == userId && x.IsBought == false)
                                                 .ToListAsync(token);
    }

    public async Task UpdateCardProductsDecrementAsync(UserProductCard userProduct, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = await _unitOfWork.Repository<Product>().Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (userProduct.Quantity >= 0)
        {
            product.Quantity += 1;

            _unitOfWork.Repository<UserProductCard>().Update(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }

    }

    public async Task UpdateCardProductsIncrementAsync(UserProductCard userProduct, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var product = await _unitOfWork.Repository<Product>().Table.SingleOrDefaultAsync(x => x.Id == userProduct.ProductId);

        if (product.Quantity > 0)
        {
            product.Quantity -= 1;

            _unitOfWork.Repository<UserProductCard>().Update(userProduct);

            await _unitOfWork.SaveChangeAsync();
        }
        else
        {
            throw new Exception("Product Quantity is not enough");
        }
    }
}
