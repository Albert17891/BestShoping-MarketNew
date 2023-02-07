using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IList<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Table.ToListAsync(cancellationToken);

        return products;
    }
}
