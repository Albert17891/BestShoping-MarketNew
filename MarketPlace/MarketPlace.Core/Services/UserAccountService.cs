using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
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

       await StartTransaction(buyerAccount, buyProductServiceResponse);

    }

    private async Task StartTransaction(UserAccount buyerAccount, BuyProductServiceModel buyProductServiceResponse)
    {
        foreach (var buyProduct in buyProductServiceResponse.BuyProducts)
        {
            var product = await _unitOfWork.Repository<Product>().Table.FirstOrDefaultAsync(x => x.Id == buyProduct.ProductId);

            var selleAccount = await _unitOfWork.Repository<UserAccount>().Table.FirstOrDefaultAsync(x => x.UserId == product.OwnerUserId);

            var productInUserCard=await _unitOfWork.Repository<UserProductCard>().Table.FirstOrDefaultAsync(x => x.Id == buyProduct.Id);

            if (selleAccount is null)
                throw new NullReferenceException("UserAccount is Not Found");

           if(productInUserCard is null)
                throw new NullReferenceException("UserProduct is Not Found");

            await CreateTransactionAsync(buyProduct.Price,buyerAccount, selleAccount, productInUserCard);

            
        }
    }

    private async Task CreateTransactionAsync(double productPrice,UserAccount buyerAccount, UserAccount selleAccount, UserProductCard? productInUserCard)
    {
        var vaucer =await GetVaucerAsync(buyerAccount.UserId,productInUserCard.ProductId);

        if (vaucer is null)
        {
            if (buyerAccount.Amount < productPrice)
                throw new ArgumentException("Amount is not Enough");


            buyerAccount.Amount -= productPrice;
            selleAccount.Amount += productPrice;

            productInUserCard.IsBought = true;
            productInUserCard.BoughtTime = DateTime.Now;

            var transaction = new Transaction
            {
                UserId = buyerAccount.UserId,
                ReceiverUserId = selleAccount.UserId,
                ProductId = productInUserCard.ProductId,
                IsUsedVaucer = false,
                TransactionPrice = productPrice,
                TransactionTime = DateTime.Now
            };

            await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

            await _unitOfWork.SaveChangeAsync();
        }
        else
        {
           // productPrice -= vaucer.Price;    //Use Vaucer 

            if (buyerAccount.Amount < productPrice)
                throw new ArgumentException("Amount is not Enough");


            buyerAccount.Amount -= productPrice;
            selleAccount.Amount += productPrice;

            productInUserCard.IsBought = true;
            productInUserCard.BoughtTime = DateTime.Now;

            var transaction = new Transaction
            {
                UserId = buyerAccount.UserId,
                ReceiverUserId = selleAccount.UserId,
                ProductId = productInUserCard.ProductId,
                IsUsedVaucer = true,
                VaucerPrice=vaucer.Price,
                TransactionPrice = productPrice,
                TransactionTime = DateTime.Now
            };

            await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

            vaucer.IsUsed = true;//Use Vaucer 
            vaucer.IsBlocked = false;

            await _unitOfWork.SaveChangeAsync();
        }
    }


    //Chech if Vaucer exist
    private async Task<Vaucer> GetVaucerAsync(string userId,int productId)
    {
       var vaucer=await _unitOfWork.Repository<Vaucer>().Table.Where(x => x.IsBlocked == true && x.IsUsed == false)
                                                    .Where(x => x.ExpireTime >= DateTime.Now)
                                                    .SingleOrDefaultAsync(x => x.UserId == userId&&x.ProductId==productId);

        return vaucer;                            
    }

    public async Task<UserAccount> GetUserAmountAsync(string userId, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<UserAccount>().Table.FirstOrDefaultAsync(x => x.UserId == userId);

        return account;
    }
}
