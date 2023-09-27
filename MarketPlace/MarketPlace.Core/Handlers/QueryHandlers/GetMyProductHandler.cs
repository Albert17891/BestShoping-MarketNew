using Dapper;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.DapperInterfaces;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Queries;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Handlers.QueryHandlers;

public class GetMyProductHandler : IRequestHandler<GetMyProductQuery, IList<Product>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetMyProductHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    //private readonly IUnitOfWork _unitOfWork;

    //public GetMyProductHandler(IUnitOfWork unitOfWork)
    //{
    //    _unitOfWork = unitOfWork;
    //}

    //public async Task<IList<Product>> Handle(GetMyProductQuery request, CancellationToken cancellationToken)
    //{
    //    var products = await _unitOfWork.Repository<Product>().Table.Where(x => x.OwnerUserId == request.UserId)
    //                                                          .ToListAsync(cancellationToken);

    //    return products;
    //}
    public async Task<IList<Product>> Handle(GetMyProductQuery request, CancellationToken cancellationToken)
    {
        await using  var sqlConnection = _connectionFactory.CreateConnection();

        var products = await sqlConnection.QueryAsync<Product>(
             @"SELECT * 
                 From Products
                  Where OwnerUserId=@UserId",
        new
        {
            request.UserId
        }) ;

        return products.ToList();
    }
}
