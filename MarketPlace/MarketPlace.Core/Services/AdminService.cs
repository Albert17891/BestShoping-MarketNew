using MarketPlace.Core.Entities;
using MarketPlace.Core.Exceptions;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public AdminService(UserManager<AppUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<bool> DeleteUserAsync(AppUser user, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var existUser = await _userManager.FindByEmailAsync(user.Email);

        if (existUser == null)
            throw new UserNotFoundException("User Not Found ");

        var result = await _userManager.DeleteAsync(existUser);       

        return result.Succeeded;
    }

    public async Task<IList<ProductWithOwner>> GetProductWithOwnersAsync(CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        var users = await _userManager.Users.ToListAsync();

        var productWithOwners = new List<ProductWithOwner>();

        foreach (var user in users)
        {
           var filterResult=result.Where(x => x.OwnerUserId == user.Id)
                 .Select(x => new ProductWithOwner
                 {
                     ProductId=x.Id,
                     Name = x.Name,
                     Quantity = x.Quantity,
                     Price = x.Price,
                     FirstName = user.FirstName,
                     Email = user.Email
                 }).ToList();

            productWithOwners.AddRange(filterResult);
        }
        return productWithOwners;
    }

    public async Task<bool> UpdateUserAsync(AppUser user, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var existUser = await _userManager.FindByEmailAsync(user.Email);

        if (user == null)
            throw new UserNotFoundException("User Not Found ");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;

        var result = await _userManager.UpdateAsync(existUser);

        return result.Succeeded;
    }


}
