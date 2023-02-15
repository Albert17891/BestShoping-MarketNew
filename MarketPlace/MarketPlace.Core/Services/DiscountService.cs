﻿using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Discount;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class DiscountService : IDiscountService
{
    private readonly IUnitOfWork _unitOfWork;  

    public DiscountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IList<DiscountCheckResponse>> CheckDiscountAsync(CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Table.Where(x => x.IsDiscount == true)
                                                                  .ToListAsync(cancellationToken);

        var results = new List<DiscountCheckResponse>();

        foreach (var product in products)
        {
           var result= await CheckDiscountStatus(product);

            results.Add(result);
        }
        return results;
    }

    private async Task<DiscountCheckResponse> CheckDiscountStatus(Product product)
    {
        if (product.IsDiscountActive&&product.DiscountTimeEnd<DateTime.Now)
        {
            product.IsDiscount = false;
            product.IsDiscountActive = false;
            var discount = (product.Price * product.DiscountPercent) / (100 - product.DiscountPercent);
            product.Price += discount;

            await _unitOfWork.SaveChangeAsync();

            return new DiscountCheckResponse { Message = $"Deactivate Discount {product.Name}" };
        }
        else if (!product.IsDiscountActive && DateTime.Now > product.DiscountTimeStart)
        {
            product.IsDiscountActive = true;
            var discount = (product.Price * product.DiscountPercent) / 100;
            product.Price -= discount;

            await _unitOfWork.SaveChangeAsync();

            return new DiscountCheckResponse { Message = $"Activate Discount {product.Name}" };
        }

        return new DiscountCheckResponse { Message = $"Not Have Any Discount " };
    }

    public async Task CreateDiscountAsync(DiscountRequestServiceModel discountRequest, CancellationToken token)
    {
        var product = await _unitOfWork.Repository<Product>().Table.SingleOrDefaultAsync(x => x.Id == discountRequest.ProductId, token);

        if (product is null)
            throw new NullReferenceException("product is not exist");

        product.IsDiscount = true;
        product.DiscountTimeStart = discountRequest.StartTime;
        product.DiscountTimeEnd = discountRequest.EndTime;
        product.DiscountPercent = discountRequest.Percent;

        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<IList<DiscountResponseServiceModel>> GetDiscountsAsync(CancellationToken token)
    {
        var products = await _unitOfWork.Repository<Product>().Table.Where(x => x.IsDiscountActive == true)
                                                                    .Select(x=>new DiscountResponseServiceModel
                                                                    {
                                                                        ProductId=x.Id,
                                                                        EndTime=x.DiscountTimeEnd,
                                                                    })
                                                                    .ToListAsync(token);

        return products;
    }
}