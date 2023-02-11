using MarketPlace.Core.Entities.Admin;

namespace MarketPlace.Core.Interfaces.Services;
public interface IVaucerService
{
    Task<IList<Vaucer>> GetVaucersByUserIdAsync(string userId, CancellationToken cancellationToken);
}
