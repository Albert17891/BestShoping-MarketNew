using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Services;

public interface ICardService
{
    Task<IList<UserProductCard>> GetCardProductsAsync(string userId,CancellationToken token);
    Task AddCardProductsAsync(UserProductCard userProduct,CancellationToken token);
    Task UpdateCardProductsIncrementAsync(UserProductCard userProduct, CancellationToken token);
    Task UpdateCardProductsDecrementAsync(UserProductCard userProduct, CancellationToken token);

}
