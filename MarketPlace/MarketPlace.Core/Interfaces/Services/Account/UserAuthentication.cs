using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Roles;
using MarketPlace.Core.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var roles = await _userManager.GetRolesAsync(user);
            token = _authenticationCreator.CreateToken(login.UserName, roles[0]);
        }

        return token;
    }

    public async Task<IList<AppUser>> GetUsers()
    {
        var users =await _userManager.Users.ToListAsync();
        var usersList = new List<AppUser>();

        foreach (var user in users)
        {
            var checkRoles =await _userManager.GetRolesAsync(user);
            if (checkRoles[0] == "User")
                usersList.Add(user);
        }

        return usersList;
    }

    public async Task<string> RegisterAsync(Register register)
    {
        var user = register.Adapt<AppUser>();

        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to Register");
        }

        //Add Roles
        await _userManager.AddToRoleAsync(user, RolesEnum.User.ToString());

        return user.Id;
    }
}
