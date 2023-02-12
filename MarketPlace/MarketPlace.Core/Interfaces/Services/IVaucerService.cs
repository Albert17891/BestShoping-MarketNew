using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Entities.Admin.Response;

namespace MarketPlace.Core.Interfaces.Services;
public interface IVaucerService
{
    Task<IList<Vaucer>> GetVaucersByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<VaucerUserResponse> UseVaucerAsync(VaucerServiceModel vaucerServiceModel, CancellationToken cancellationToken);
}
