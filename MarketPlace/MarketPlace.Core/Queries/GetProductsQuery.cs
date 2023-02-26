using MarketPlace.Core.Entities;
using MediatR;

namespace MarketPlace.Core.Queries;

public class GetProductsQuery:IRequest<IList<Product>>
{

}
