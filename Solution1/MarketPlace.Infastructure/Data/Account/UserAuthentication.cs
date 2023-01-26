using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Infastructure.Data.Account;

public class UserAuthentication : IUserAuthentication
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAuthenticationCreator _authenticationCreator;

    public UserAuthentication(UserManager<AppUser> userManager, IAuthenticationCreator authenticationCreator)
    {
        _userManager = userManager;
        _authenticationCreator = authenticationCreator;
    }
    public async Task<string> AuthenticateAsync(Login login)
    {
        string token = null;

        var user = await _userManager.FindByNameAsync(login.UserName);

        if (user == null)
        {
            throw new NullReferenceException("User Not Found");
        }

        var checkResult = await _userManager.CheckPasswordAsync(user, login.Password);

        if (checkResult)
        {
            token = _authenticationCreator.CreateToken(login.UserName);
        }

        return token;
    }

    public async Task<string> RegisterAsync(Register register)
    {
        var user = register.Adapt<AppUser>();

        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to Register");
        }

        return user.Id;
    }
}
