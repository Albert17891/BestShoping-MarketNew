using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Handlers.QueryHandlers;

public class GetMyProductHandler : IRequestHandler<GetMyProductQuery, IList<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMyProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Product>> Handle(GetMyProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Repository<Product>().Table.Where(x => x.OwnerUserId == request.UserId)
                                                              .ToListAsync(cancellationToken);

        return products;
    }
}
