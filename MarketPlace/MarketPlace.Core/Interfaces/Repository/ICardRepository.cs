using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Repository;

public interface ICardRepository
{
    IQueryable<UserProductCard> Table { get; }
    Task AddAsync(UserProductCard userProductCard);
}
