using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;

namespace MarketPlace.Core.Interfaces.Account;

public interface IUserAuthentication
{
    public Task<LoginResponse> AuthenticateAsync(Login login);
    public Task<string> RegisterAsync(Register register);

    public Task<IList<UserEntity>> GetUsers();
}
