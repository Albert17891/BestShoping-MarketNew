using MarketPlace.Core.Entities;
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
        var vaucers = await _unitOfWork.Repository<Vaucer>().Table
                                      .Where(x => x.UserId == userId && x.ExpireTime > DateTime.Now && x.IsUsed == false)
                                      .ToListAsync(cancellationToken);

        return vaucers;
    }

    public async Task<bool> UseVaucerAsync(VaucerServiceModel vaucerServiceModel, CancellationToken cancellationToken)
    {
        var vaucer = await _unitOfWork.Repository<Vaucer>().Table
                           .SingleOrDefaultAsync(x => x.VaucerName == vaucerServiceModel.VaucerName && x.IsUsed == false, cancellationToken);

        if (vaucer is null)
            return false;

        var result = await CheckVaucerProducts(vaucer.ProductId, vaucerServiceModel.UserId);

        if (result is null)
            return false;

        result.Price -= vaucer.Price;
        vaucer.IsUsed = true;

        await _unitOfWork.SaveChangeAsync();

        return true;
    }

    private async Task<UserProductCard> CheckVaucerProducts(int productId, string userId)
    {
        var product = await _unitOfWork.Repository<UserProductCard>().Table.Where(x => x.UserId == userId && x.ProductId == productId)
                                                                            .SingleOrDefaultAsync();

        if (product is null)
            throw new NullReferenceException("Product Not Found");

        return product;
    }
}
