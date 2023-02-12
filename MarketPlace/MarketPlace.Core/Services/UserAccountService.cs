using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.User;
using MarketPlace.Core.Handlers.QueryHandlers;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public UserAccountService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task BuyProductAsync(BuyProductServiceModel buyProductServiceResponse, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(buyProductServiceResponse.UserId);

        if (user is null)
            throw new NullReferenceException("User Not Found");

        var buyerAccount = await _unitOfWork.Repository<UserAccount>().Table
                                            .FirstOrDefaultAsync(x => x.UserId == buyProductServiceResponse.UserId);

        if (buyerAccount is null)
            throw new NullReferenceException("User Account Not Found");

       await MakeTransaction(buyerAccount, buyProductServiceResponse);

    }

    private async Task MakeTransaction(UserAccount buyerAccount, BuyProductServiceModel buyProductServiceResponse)
    {
        foreach (var buyProduct in buyProductServiceResponse.BuyProducts)
        {
            var product = await _unitOfWork.Repository<Product>().Table.FirstOrDefaultAsync(x => x.Id == buyProduct.ProductId);

            var selleAccount = await _unitOfWork.Repository<UserAccount>().Table.FirstOrDefaultAsync(x => x.UserId == product.OwnerUserId);

            var productInUserCard=await _unitOfWork.Repository<UserProductCard>().Table.FirstOrDefaultAsync(x => x.Id == buyProduct.Id);

            if (selleAccount is null)
                throw new NullReferenceException("UserAccount is Not Found");

            if (buyerAccount.Amount < buyProduct.Price)
                throw new ArgumentException("Amount is not Enough");

            buyerAccount.Amount -= buyProduct.Price;
            selleAccount.Amount += buyProduct.Price;

            productInUserCard.IsBought = true;
            productInUserCard.BoughtTime = DateTime.Now;

            await _unitOfWork.SaveChangeAsync();
        }
    }   

    public async Task<UserAccount> GetUserAmountAsync(string userId, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<UserAccount>().Table.FirstOrDefaultAsync(x => x.UserId == userId);

        return account;
    }
}
