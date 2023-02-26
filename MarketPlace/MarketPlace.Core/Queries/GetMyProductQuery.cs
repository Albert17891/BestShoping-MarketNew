using MarketPlace.Core.Entities;
using MediatR;

namespace MarketPlace.Core.Queries;

public class GetMyProductQuery:IRequest<IList<Product>>
{
	public string UserId { get; }
	public GetMyProductQuery(string userId)
	{
		UserId = userId;
	}
}
