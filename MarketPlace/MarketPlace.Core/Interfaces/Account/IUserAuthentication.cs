using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Account;

public interface IUserAuthentication
{
    public Task<LoginResponse> AuthenticateAsync(Login login);
    public Task<string> RegisterAsync(Register register);

    public Task<IList<AppUser>> GetUsers();
}
