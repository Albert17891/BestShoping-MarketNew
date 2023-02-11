using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class VaucerService : IVaucerService
{
    private readonly IUnitOfWork _unitOfWork;

    public VaucerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Vaucer>> GetVaucersByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var vaucers =await _unitOfWork.Repository<Vaucer>().Table
                                      .Where(x => x.UserId == userId && x.ExpireTime > DateTime.Now&&x.IsUsed==false)
                                      .ToListAsync(cancellationToken);

        return vaucers;
    }
}
