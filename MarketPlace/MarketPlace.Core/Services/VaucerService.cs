﻿using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Entities.Admin.Response;
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

    public async Task<IList<VaucerShow>> GetVaucerByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        var vaucers = await _unitOfWork.Repository<Vaucer>().Table.Include(x=>x.AppUser).Where(x => x.ProductId == productId)
                                                                  .Select(x=>new VaucerShow
                                                                  {
                                                                      UserName=x.AppUser.FirstName,
                                                                      ExpireTime=x.ExpireTime,
                                                                      IsUsed=x.IsUsed
                                                                  })
                                                                  .ToListAsync(cancellationToken);

        return vaucers;
    }

    public async Task<IList<Vaucer>> GetVaucersByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var vaucers = await _unitOfWork.Repository<Vaucer>().Table
                                      .Where(x => x.UserId == userId && x.ExpireTime > DateTime.Now && x.IsUsed == false)
                                      .ToListAsync(cancellationToken);

        return vaucers;
    }

    public async Task<VaucerUserResponse> UseVaucerAsync(VaucerServiceModel vaucerServiceModel, CancellationToken cancellationToken)
    {
        var vaucer = await _unitOfWork.Repository<Vaucer>().Table
                           .Where(x => x.ExpireTime >= DateTime.Now&&x.IsBlocked==false)
                           .SingleOrDefaultAsync(x => x.VaucerName == vaucerServiceModel.VaucerName && x.IsUsed == false, cancellationToken);

        if (vaucer is null)
            return new VaucerUserResponse() { Status = false };

        var result = await CheckVaucerProducts(vaucerServiceModel.Id, vaucerServiceModel.UserId);

        if (result is null)
            return new VaucerUserResponse() { Status = false };


        result.SumPrice -= vaucer.Price;      
        vaucer.IsBlocked = true;

        await _unitOfWork.SaveChangeAsync();

        return new VaucerUserResponse() { Status = true, Price = vaucer.Price };
    }

    private async Task<UserProductCard> CheckVaucerProducts(int Id, string userId)
    {
        //Check Products if it is exist in UserProductCart 
        var product = await _unitOfWork.Repository<UserProductCard>().Table.Where(x => x.UserId == userId && x.Id == Id)
                                                                            .SingleOrDefaultAsync();

        if (product is null)
            throw new NullReferenceException("Product Not Found");

        return product;
    }
}
