using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Account;

public interface IAuthenticationCreator
{
    string CreateToken(string userName,string role);
}
