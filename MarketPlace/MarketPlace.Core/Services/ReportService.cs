using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin.Report;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace MarketPlace.Core.Services;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public ReportService(IUnitOfWork unitOfWork,IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }
    public async Task<IList<TopProduct>> GetTopTenProductsAsync(CancellationToken cancellationToken)
    {
        var topProducts = await _unitOfWork.Repository<UserProductCard>().Table
                                       .Where(x=>x.IsBought==true&&x.BoughtTime>DateTime.Today.AddDays(-30))
                                       .GroupBy(x => x.ProductId)
                                       .Select(x=>new TopProduct
                                        {                                         
                                         SumQuantity = x.Sum(x => x.Quantity),
                                         Name=x.Select(x=>x.Name).First(),
                                         Price=x.Select(x=>x.Price).First()
                                       }).OrderByDescending(x=>x.SumQuantity).Take(10)
                                       .ToListAsync(cancellationToken);

        return topProducts;
    }

    public async Task<IList<TopUser>> GetTopTenUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<UserProductCard>().Table.Include(x => x.AppUser).Where(x => x.IsBought == true)
                                                                         .GroupBy(x => x.UserId)
                                                                         .Select(x => new TopUser
                                                                        {
                                                                             Name=x.Select(x=>x.AppUser.FirstName).First(),
                                                                             MoneySum=x.Select(x=>x.SumPrice).Sum()
                                                                         }).Take(10).ToListAsync(cancellationToken);

        return users;
    }
}
