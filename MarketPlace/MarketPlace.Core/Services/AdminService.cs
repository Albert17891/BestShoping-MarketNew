using MarketPlace.Core.Entities;
using MarketPlace.Core.Exceptions;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Core.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<AppUser> _userManager;

    public AdminService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
