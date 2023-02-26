using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.User;

namespace MarketPlace.Core.Interfaces.Services;

public interface IUserAccountService
{
    Task<UserAccount> GetUserAmountAsync(string userId, CancellationToken cancellationToken);
    Task BuyProductAsync(BuyProductServiceModel buyProductServiceResponse,CancellationToken cancellationToken);
}
