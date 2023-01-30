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

        await _unitOfWork.CardRepository.AddAsync(userProduct);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<IList<UserProductCard>> GetCardProductsAsync(string userId, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return await _unitOfWork.CardRepository.Table.Where(x => x.UserId == userId)
                                                 .ToListAsync();
    }
}
