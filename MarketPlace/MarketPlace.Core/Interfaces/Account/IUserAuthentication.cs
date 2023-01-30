using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Account;

public interface IUserAuthentication
{
    public Task<string> AuthenticateAsync(Login login);
    public Task<string> RegisterAsync(Register register);

    public Task<IList<AppUser>> GetUsers();
}
