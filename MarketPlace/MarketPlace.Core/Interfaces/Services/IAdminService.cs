using MarketPlace.Core.Entities;

namespace MarketPlace.Core.Interfaces.Services;
public interface IAdminService
{
    Task<bool> UpdateUserAsync(AppUser user, CancellationToken token);
    Task<bool> DeleteUserAsync(AppUser user, CancellationToken token);

}
