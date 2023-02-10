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
        var topProducts = await _unitOfWork.Repository<UserProductCard>().Table.GroupBy(x => x.ProductId)
                                       .Select(x=>new TopProduct
                                        {                                         
                                         SumQuantity = x.Sum(x => x.Quantity),
                                         Name=x.Select(x=>x.Name).First(),
                                         Price=x.Select(x=>x.Price).First()
                                       }).OrderByDescending(x=>x.SumQuantity)
                                       .ToListAsync(cancellationToken);

        return topProducts;
    }
}
